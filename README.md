
<p><strong><img src="https://github.com/techdata-cloudautomation/cpsso/blob/master/Marketing/GithubBanner.JPG" alt="Customer Portal SDK - StreamOne" width="990" height="380" /></strong></p>
<p><strong>What is &ldquo;Customer Portal SDK &ndash; StreamOne&rdquo;?</strong><br />Customer Portal Software Development Kit (SDK) &ndash; StreamOne, is a subscription management web application that allows
both customers and end users to update their Microsoft CSP subscriptions.<br /><br />There are two user levels:<br />&bull; The customer portal allows you to manage users, modify seat quantity and view log history, see cost prices, setup sales
margins and seat increase limits<br />&bull; The end user portal allows them to modify seat quantity and view log history, also see pre-defined sales price</p>
<p><img src="https://raw.githubusercontent.com/techdata-cloudautomation/cpsso/master/Marketing/2.JPG" alt="" width="990" height="283" /></p>
<p><strong>What are the key benefits?</strong><br />Firstly, it is very easy to use and there is no need to log into StreamOne. In StreamOne only administrators are able to manage users and modify seats. In Customer Portal SDK – StreamOne any approved users from your organisation can manage users and modify seats 24/7 on any device. Most importantly, your customers can also be given access to the
app to modify seat quantity so you no longer need to be involved in the process, saving you time and money. Lastly changes are effective in seconds at the click of a button within the app. With the web app you can manage multiple user access and also view the history of any license changes.<br/></p>
<p><strong>How does it work?</strong><br />The app is integrated to StreamOne Cloud Marketplace via an API. You and your customers log in from a browser using active directory credentials. Once you login for the first time it will collate all your end user details and their CSP subscriptions and display them together on one screen. The Customer Portal SDK – Streamone is currently available as a Software Development Kit (SDK). Using Github Repository and the power of Azure Resource Management (ARM) you can easily deploy it into your account. This application leverages the power of Azure for you to self-host it and easily maintain it after deployment.</p>
<p><strong>When will it be available?</strong><br />Customer Portal SDK &ndash; StreamOne is available now.</p>
<p><strong>Who will it be available to?</strong><br />It is available to all customers that have basic Azure knowledge and an Azure account.</p>
<p><strong>What does Customer Portal SDK - StreamOne look like?</strong><br />It looks great! It has a very simple and modern looking Graphical User Interface (GUI).</p>
<p><img src="https://github.com/techdata-cloudautomation/cpsso/blob/master/Marketing/9_Customer%20Portal%20SDK%20View.JPG?raw=true" alt="" width="770" height="410" /></p>
<p><strong>What are the customer pre-requisites?</strong><br />&bull; Customer Portal SDK StreamOne License Terms<br/>&bull; Partner API credentials<br />&bull; Azure account<br />&bull; Azure CSP subscription<br />&bull; Microsoft account for login<br />&bull; Partner resources to maintain it on Azure <br />&bull; Estimate monthly cost ~USD18.00 (List Price, VAT Excluded)<br /> App Service, Shared Tier, D1, 1 GB RAM, 1 GB  Storage<br /> Azure SQL DB Single Database, Basic Tier. 5 DTUs, 2, 2 GM </p>
<p><strong>How to get onboarded?</strong><br />Contact SCM Customer Integrations Team according of your region:</p>
<p><strong>EUROPE</strong><br /><a href="mailto:EU.TDCloud@Techdata.com">EU.TDCloud@Techdata.com</a><br /><strong>US</strong><br /><a href="mailto:PartnerApi@Techdata.com">PartnerApi@Techdata.com</a><br /><strong>CANADA</strong><br /><a href="mailto:StreamOne@Techdata.ca">StreamOne@Techdata.ca</a></p>
<p><strong>Summary - Deployment using Github and Powershell Script</strong><br />We have accomplished the automation of 100% of the deployment of this SDK application. Tech Data Business Partners with basic Azue knowledge will be able to self-deploy the App. The first step is to dowload the Deployment Script at the botton of the page and use power shell to deploy the application. The process will take around 17 minutes;</p>
<p><strong>Auto Deployment Information</strong><br />For further details on how to deploy the application please read the file AutoDeploy. Customer Portal SDK StreamOne.</p>
<p><strong>IMPORTANT</strong><br />Before you proceed make sure you have read the Auto Deployment document and make sure you have all the necessary information for the input fields in the powershell instalation.</p>
<!--<p><img src="https://github.com/techdata-cloudautomation/cpsso/blob/master/Marketing/8.jpg?raw=true" alt="" width="645" height="772" /></p>-->
<p><strong>Step by Step for Deployment</strong></p>
<p>1. Downloaded the Script</p>
<p>2. Visit <u><a href="https://shell.azure.com/">https://shell.azure.com/</a></u></p>
<p>3. Upload File</p>
<p>4. Type in: cd $home</p>
<p>6. Type in: ./cpssodeploy.ps1</p>
<p>7. Authenticate with password</p>
<p>8. Place the Setting as you are requested:</p>
<p><strong>SETTINGS</strong></p>
<p><strong>Resource Group Name:</strong>&nbsp;</p>
<p><strong>Site </strong><strong>Name:</strong>&nbsp;<em>your App URL.</em> C<em>hoose the subdomain of your choise that will come before&nbsp;(.azurewebsites.net)</em></p>
<p><strong>Location: </strong><em>ex: West Europe, East US, Central Canada, etc</em></p>
<p><strong>SQL </strong><strong>password:</strong>&nbsp; c<em>reate one as you please</em></p>
<p><strong>Allowed Resellers:</strong>&nbsp;e-mail to get initial access to the Application. E<em>-mail from the one installing the App</em></p>
<p><strong>Client ID:</strong> <em>Provided by Customer Integrations Team</em></p>
<p><strong>Client </strong><strong>secret</strong>: <em>Provided by Customer Integrations Team</em></p>
<p><strong>Reseller </strong><strong>ID:</strong> <em>Provided by Customer Integrations Team</em></p>
<p><strong>SOIN: </strong><em>Provided by Customer Integrations Team</em></p>
<p><strong>Reseller </strong><strong>name: </strong><em>your company name</em></p>
<p><strong>Reseller </strong><strong>region:&nbsp;</strong><em>ex: EUROPE, US or CANADA. Capital letters only..</em></p>
<p><strong>Notification </strong><strong>emails: </strong><em>e-mail to receive seat change notifications</em></p>
<p><strong>Notification email from:&nbsp;</strong><em>e-mail to trigger notifications</em></p>
<p><strong>Notification email password:&nbsp;</strong><em>password of above e-mail</em></p>
<p>&nbsp;<img src="https://github.com/techdata-cloudautomation/cpsso/blob/master/Marketing/5.jpg?raw=true" alt="" width="500" height="333" /></p>
<p>&nbsp;<strong>Support Documents</strong></p>
<p>Support Documents are to be found in the root of the application</p>
<ul>
<li><a title="Customer Portal SDK Deplyment Document.pdf" href="https://github.com/techdata-cloudautomation/cpsso/blob/master/SupportDocs/Customer%20Portal%20SDK%20StreamOne%20Deplyment%20Doc.pdf">Customer Portal SDK Deplyment Document.pdf</a></li>
<li><a title="User Manual Customer Portal SDK StreamOne 1.4.pdf" href="https://github.com/techdata-cloudautomation/cpsso/blob/master/SupportDocs/User%20Manual%20Customer%20Portal%20SDK%20StreamOne%201.4.pdf">User Manual Customer Portal SDK StreamOne 1.4.pdf</a></li>
<li><a title="Import Source Code To Visual Studio.pdf" href="https://github.com/techdata-cloudautomation/cpsso/blob/master/SupportDocs/Import%20Source%20Code%20To%20Visual%20Studio.pdf">Import Source Code To Visual Studio.pdf</a></li>
<li><a title="Customer Portal SDK Manual Deployment Document.pdf" href="https://github.com/techdata-cloudautomation/cpsso/blob/master/SupportDocs/Manual%20Deployment%20Technical%20Doc%20SDK.pdf">Customer Portal SDK Manual Deployment Document.pdf</a></li>
</ul>
<p><strong>Support</strong></p>
<p>What is the process for addressing issues that arise?</p>
<p>Tech Data provides limited support for this application as it is hosted in our customer&rsquo;s private environment that we do not control.</p>
<p>Being an Open Source Code further development of the application to your needs is possible. Such self-development activities are not supported and could potentially block your use of future versions of the app.</p>
<p>Sending an e-mail will kick off the following process:</p>
<ul>
<li>Your message will be acknowledged within one business day.</li>
<li>Within the following business day, we will review your concern.</li>
<li>If needed we will pull in additional resources to gain further insight and provide guidance.</li>
<li>The team will work with you to come to a conclusion.</li>
</ul>
<p>While issue complexity varies, the goal is to resolve issues within five working days: Monday-Friday: Business Hours.</p>
<p>All communication will be confidential with very limited circulation.</p>
<p>Support request emails should be sent to each team in accordance with your region:</p>
<p><strong>EUROPE</strong><br /> <a href="mailto:EU.API.Support@TechData.com">EU.API.Support@TechData.com</a><br /> <strong>US</strong><br /><a href="mailto:PartnerApi@Techdata.com">PartnerApi@Techdata.com</a><br /> <strong>CANADA</strong><br /><a href="mailto:StreamOne@Techdata.ca">StreamOne@Techdata.ca</a></p>
<p><img src="https://github.com/techdata-cloudautomation/cpsso/blob/master/Marketing/6.jpg?raw=true" alt="" width="700" height="431" /></p>
<p><strong>Installation Deployment</strong></p>
<!--<a href="https://azuredeploy.net/" target="_blank">
   <img src="http://azuredeploy.net/deploybutton.png"/>
</a>
-->
<a class="github-button" href="https://github.com/techdata-cloudautomation/cpsso/blob/master/Deploy%20Script/cpssodeploy.zip?raw=true" data-icon="octicon-cloud-download" aria-label="Download ntkme/github-buttons on GitHub">Download Installation Script</a>

