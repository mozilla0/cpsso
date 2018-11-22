using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateLabelLite.Services.Caching;
using PrivateLabelLite.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using PrivateLabelLite.Entities.Common;
using PrivateLabelLite.Entities.EndUser;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PrivateLabelLite.Entities.Api;
using PrivateLabelLite.Services.Settings;
using PrivateLabelLite.Entities.Product;
using PrivateLabelLite.Entities.Subsciptions;
namespace PrivateLabelLite.Services.PartnerApi
{
    public class PartnerApi : IPartnerApi
    {
        private readonly ICacheService _cacheService;
        public PartnerApi(ICacheService cachingService, ISettingsService settingsService)
        {
            _cacheService = cachingService;
        }
        public string GetCamelCaseJsonString(object obj)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }
        public T Post<T>(string uri, List<KeyValuePair<string, string>> headers, object postObj)
        {
            var apiResponse = CallApi(uri, postObj, headers, HttpMethod.Post);
            return apiResponse.IsSuccess ? JsonConvert.DeserializeObject<T>(apiResponse.ResponseJson) : (T)Activator.CreateInstance(typeof(T));
        }
        public T Patch<T>(string uri, List<KeyValuePair<string, string>> headers, object postObj)
        {
            var apiResponse = CallApi(uri, postObj, headers, new HttpMethod("PATCH"));
            return apiResponse.IsSuccess ? JsonConvert.DeserializeObject<T>(apiResponse.ResponseJson) : (T)Activator.CreateInstance(typeof(T));
        }
        public T Get<T>(string uri, List<KeyValuePair<string, string>> headers = null)
        {
            var apiResponse = CallApi(uri, null, headers, HttpMethod.Get);
            return JsonConvert.DeserializeObject<T>(apiResponse.ResponseJson);
        }

        private ApiResponse CallApi(string uri, object postObj, List<KeyValuePair<string, string>> headers, HttpMethod method)
        {
            ApiResponse apiResponse = new ApiResponse();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var requestJson = GetCamelCaseJsonString(postObj);
                HttpRequestMessage request = null;
                if (method == HttpMethod.Get)
                {
                    request = new HttpRequestMessage(method, uri) { };

                    // From: https://stackoverflow.com/questions/26109650/explicitly-set-content-type-headers-for-get-operation-in-httpclient/28630077
                    // HACK: Set 'Content-Type' even for GET requests
                    var invalidHeaders = (HashSet<string>)typeof(System.Net.Http.Headers.HttpHeaders)
                        .GetField("invalidHeaders", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        .GetValue(request.Headers);
                    invalidHeaders.Remove("Content-Type");
                    request.Headers.Add("Content-Type", "application/json");
                }
                else
                {
                    request = new HttpRequestMessage(method, uri) { Content = new StringContent(requestJson) };
                    request.Content.Headers.Clear();
                    request.Content.Headers.Add("Content-Type", "application/json");
                }
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpResponseMessage result = client.SendAsync(request).Result;


                dynamic respData = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                if (respData != null)
                {
                    apiResponse.IsSuccess = respData.Result == "Success" ? true : false;
                    apiResponse.ResponseJson = apiResponse.IsSuccess ? respData.BodyText.ToString() : "";
                }
                if (!apiResponse.IsSuccess)
                {
                    //ApiErrorException apiError = JsonConvert.DeserializeObject<ApiErrorException>(respData.ToString());
                    //throw apiError;
                }
                return apiResponse;
            }
        }
        public AccessToken GenerateToken(string url = null, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> bodyItems = null)
        {
            var _cacheKey = "access-token-oauth";
            url = url ?? ConfigKeys.TokenOAuth_URL;
            if (bodyItems == null)
            {
                bodyItems = new List<KeyValuePair<string, string>>();
                bodyItems.Add(new KeyValuePair<string, string>("client_id", ConfigKeys.client_id));
                bodyItems.Add(new KeyValuePair<string, string>("client_secret", ConfigKeys.client_secret));
                bodyItems.Add(new KeyValuePair<string, string>("grant_type", ConfigKeys.grant_type));
            }
            var token = _cacheService.Get<AccessToken>(_cacheKey);
            if (token != null)
            {
                return token;
            }
            var responseJson = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(bodyItems) };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpResponseMessage result = client.SendAsync(request).Result;
                responseJson = result.Content.ReadAsStringAsync().Result;
            }
            token = JsonConvert.DeserializeObject<AccessToken>(responseJson);
            var cacheTimeMin = (token.Expires_In / 60) - 2; // subtract 2 mins for surety
            _cacheService.Set(_cacheKey, token, cacheTimeMin);
            return token;
        }

        public string GetTimeStamp()
        {
            var timeStamp = DateTime.Now.Millisecond.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
                             + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString()
                             + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            return timeStamp;
        }

        public List<KeyValuePair<string, string>> GenerateAuthorizationHeaders()
        {
            var accessToken = GenerateToken();
            var timeStamp = GetTimeStamp();
            var signature = accessToken.Access_Token + ":" + ConfigKeys.SOIN + ":" + timeStamp;
            var signature_Bytes = System.Text.Encoding.UTF8.GetBytes(signature);
            var signature_Base64_Encoded = System.Convert.ToBase64String(signature_Bytes);

            var authHeaders = new List<KeyValuePair<string, string>>();
            authHeaders.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + accessToken.Access_Token));
            authHeaders.Add(new KeyValuePair<string, string>("SOIN", ConfigKeys.SOIN));
            authHeaders.Add(new KeyValuePair<string, string>("TimeStamp", timeStamp));
            authHeaders.Add(new KeyValuePair<string, string>("Signature", signature_Base64_Encoded));
            authHeaders.Add(new KeyValuePair<string, string>("Accept", "application/json"));

            return authHeaders;
        }

        public CustomersDetail GetCustomersDetail(EndCustomerFilter filter)
        {
            return Post<CustomersDetail>(ConfigKeys.CustomerSearchURL, GenerateAuthorizationHeaders(), filter);
        }

        public CustomersDetail GetAllCustomers()
        {
            var headers = GenerateAuthorizationHeaders();
            var customerSearchURL = ConfigKeys.CustomerSearchURL;
            EndCustomerFilter filter = new EndCustomerFilter()
            {
                Page = 1
            };
            var customers = Post<CustomersDetail>(customerSearchURL, headers, filter);
            for (int i = 2; i <= customers.TotalPages; i++)
            {
                filter.Page = i;
                var nextPage = Post<CustomersDetail>(customerSearchURL, headers, filter);
                customers.EndCustomersDetails.AddRange(nextPage.EndCustomersDetails);
            }
            return customers;
        }

        public List<string> GetAllCompanies()
        {
            var customer = GetAllCustomers();
            var companies = new List<string>();
            if (customer != null && customer.EndCustomersDetails != null)
            {
                companies = customer.EndCustomersDetails.Select(x => x.CompanyName).Distinct().ToList();
            }
            return companies;
        }

        public Entities.Order.OrderSearchResult GetOrders(Entities.Order.OrderFilter filter)
        {
            return Post<Entities.Order.OrderSearchResult>(ConfigKeys.OrderSearchUrl, GenerateAuthorizationHeaders(), filter);
        }


        public Entities.Order.ModifyOrderResult ModifyOrder(Entities.Order.ModifyOrder modifiedOrder)
        {
            return Patch<Entities.Order.ModifyOrderResult>(ConfigKeys.OrderModifyUrl, GenerateAuthorizationHeaders(), modifiedOrder);
        }

        public Lines GetProductPriceInfo(List<Lines> lines)
        {
            return Post<Lines>(ConfigKeys.ProductPricingUrl, GenerateAuthorizationHeaders(), lines);
        }

        public Entities.Order.OrderDetailResult GetOrderDetail(string orderNumber)
        {
            var orderDetailUrl = ConfigKeys.OrderDetailUrl ?? "";
            orderDetailUrl = orderDetailUrl.LastIndexOf('/') == (orderDetailUrl.Length - 1) ? orderDetailUrl + orderNumber : orderDetailUrl + "/" + orderNumber;
            return Get<Entities.Order.OrderDetailResult>(orderDetailUrl, GenerateAuthorizationHeaders());
        }

        public Entities.Order.OrdersDetailsResult GetOrdersDetails(SubscriptionDetail subscriptionDetail)
        {
            var ordersDetailsResult = new Entities.Order.OrdersDetailsResult()
            {
                OrdersInfoResult = new List<Entities.Order.OrderDetailResult>()
            };
            var headers = GenerateAuthorizationHeaders();
            var orderDetailUrl = ConfigKeys.OrderDetailUrl ?? "";
            string getOrderUrls = "";
            foreach (var orderNumber in subscriptionDetail.OrderNumbers)
            {
                getOrderUrls = orderDetailUrl.LastIndexOf('/') == (orderDetailUrl.Length - 1) ? orderDetailUrl + orderNumber : orderDetailUrl + "/" + orderNumber;
                var orderInfo = Get<Entities.Order.OrderDetailResult>(getOrderUrls, headers);
                if (orderInfo != null)
                    ordersDetailsResult.OrdersInfoResult.Add(orderInfo);
            }
            return ordersDetailsResult;
        }

        public Entities.Subsciptions.SubscriptionDetailResult GetSubscriptiondetail()
        {
            var subscriptionSearchUrl = ConfigKeys.SubscriptionSearchUrl ?? "";
            var headers = GenerateAuthorizationHeaders();
            var searchUrl = subscriptionSearchUrl + 1;
            var subscriptions = Get<SubscriptionDetailResult>(searchUrl, headers);
            var totalPages = subscriptions.totalPages;
            for (int i = 2; i <= totalPages; i++)
            {
                searchUrl = subscriptionSearchUrl + i;
                var temp = Get<SubscriptionDetailResult>(searchUrl, headers);
                if (temp != null)
                {
                    if (temp.Subscriptions != null)
                        subscriptions.Subscriptions.AddRange(temp.Subscriptions);
                }
            }
            return subscriptions;
        }

        public Entities.Order.ModifyOrderAddonsResult ModifyOrderAddOns(Entities.Order.ModifyOrderAddons modifiedAddOns)
        {
            return Patch<Entities.Order.ModifyOrderAddonsResult>(ConfigKeys.ModifyAddOnUrl, GenerateAuthorizationHeaders(), modifiedAddOns);
        }

        public Entities.Vendor.VendorList GetVendors()
        {
            var vendorList = Get<Entities.Vendor.VendorList>(ConfigKeys.VendorListUrl, GenerateAuthorizationHeaders());
            return vendorList;
        }

        public Entities.Product.VendorCatalogSearchResult GetProductsByVendor(Entities.Product.VendorCatalogSearch filter)
        {
            return Post<Entities.Product.VendorCatalogSearchResult>(ConfigKeys.VendorCatalogSearchUrl, GenerateAuthorizationHeaders(), filter);
        }

        public Entities.Product.VendorCatalogSearchResult GetAllProductsByVendor(Entities.Product.VendorCatalogSearch filter)
        {
            if (filter == null || filter.VendorIds == null || filter.VendorIds.Count == 0)
            {
                return null;
            }
            var vendorCatalog = Post<Entities.Product.VendorCatalogSearchResult>(ConfigKeys.VendorCatalogSearchUrl, GenerateAuthorizationHeaders(), filter);

            if (vendorCatalog != null && vendorCatalog.Products.TotalPages > 1)
            {
                // start adding next pages records
                for (int i = 2; i <= vendorCatalog.Products.TotalPages; i++)
                {
                    filter.Page = i;
                    var nextCatalogPage = Post<Entities.Product.VendorCatalogSearchResult>(ConfigKeys.VendorCatalogSearchUrl, GenerateAuthorizationHeaders(), filter);
                    if (vendorCatalog.Products.Vendors.FirstOrDefault() != null && nextCatalogPage.Products != null && nextCatalogPage.Products.Vendors != null)
                    {
                        vendorCatalog.Products.Vendors.FirstOrDefault().Listings
                        .AddRange(
                                nextCatalogPage.Products.Vendors.FirstOrDefault().Listings
                                    .Where(
                                            z => !vendorCatalog.Products.Vendors.FirstOrDefault().Listings
                                            .Exists(l => l.ListingName == z.ListingName)
                                        )
                        );
                        for (int j = 0; j < vendorCatalog.Products.Vendors.FirstOrDefault().Listings.Count; j++)
                        {
                            var listing = vendorCatalog.Products.Vendors.FirstOrDefault().Listings[j];
                            var matchedListing = nextCatalogPage.Products.Vendors.FirstOrDefault().Listings.Where(y => y.ListingName == listing.ListingName).FirstOrDefault();
                            if (matchedListing != null && matchedListing.skus != null)
                            {
                                //add unmatched skus
                                var unMatchedSkus = matchedListing.skus.Where(z => listing.skus.Any(s => s.sku != z.sku)).ToList();
                                listing.skus = listing.skus.Union(unMatchedSkus).ToList();
                            }
                        }
                    }
                }

            }
            return vendorCatalog;
        }


        public Entities.Vendor.VendorDetail GetVendorMicrosoft()
        {
            return GetVendors().Vendors.Where(x => (x.Name ?? "").ToLower() == Vendors.Microsoft.ToString().ToLower()).FirstOrDefault();
        }

        public Entities.Product.Catalogue GetMicrosoftVendorCatalogue()
        {
            var _cacheKey = "Microsoft-vendor-catalog-cached-1";

            var vendorCatalogListing = _cacheService.Get<Entities.Product.Catalogue>(_cacheKey);
            if (vendorCatalogListing != null)
            {
                return vendorCatalogListing;
            }
            else
            {
                vendorCatalogListing = new Catalogue();
            }

            var vendorMicrosoft = GetVendorMicrosoft();
            var vendorCatalogSearchFilter = new VendorCatalogSearch();
            if (vendorMicrosoft != null)
            {
                vendorCatalogSearchFilter.VendorIds = new List<string>();
                vendorCatalogSearchFilter.VendorIds.Add(vendorMicrosoft.Id);
            }

            var microsoftProducts = GetAllProductsByVendor(vendorCatalogSearchFilter);
            if (microsoftProducts != null && microsoftProducts.Products != null && microsoftProducts.Products.Vendors != null)
            {
                vendorCatalogListing.VendorCatalogue = microsoftProducts.Products.Vendors.FirstOrDefault();
                //var cacheTimeMin = ConfigKeys.MSVendorCatalogIntervalMin;
                _cacheService.Set(_cacheKey, vendorCatalogListing, 30);
            }
            return vendorCatalogListing;
        }
    }
}
