<p><strong><img src="https://raw.githubusercontent.com/techdata-cloudautomation/cpsso/master/Marketing/1.JPG" alt="Customer Portal SDK - StreamOne" width="990" height="247" /></strong></p>
<p><strong>What is &ldquo;Customer Portal SDK &ndash; StreamOne&rdquo;?</strong><br />Customer Portal Software Development Kit (SDK) &ndash; StreamOne, previously known as My StreamOne, is a subscription management web application that allows both customers and end users to update their Microsoft CSP subscriptions.<br /><br />There are two user levels:<br />&bull; The end user portal allows them to modify seat quantity and view log history<br />&bull; The customer portal allows you to manage users, modify seat quantity and view log history</p>
<p><img src="https://raw.githubusercontent.com/techdata-cloudautomation/cpsso/master/Marketing/2.JPG" alt="" width="990" height="283" /></p>
<p><strong>What are the key benefits?</strong><br />Firstly, it is very easy to use and there is no need to log into StreamOne. In StreamOne only administrators are able to manage users and modify seats. In Customer Portal SDK &ndash; StreamOne any approved users from your organization<br />can manage users and modify seats, 24/7 on any device. Most importantly, your customers can also be given access to the app to modify seat quantity so you no longer need to be involved in the process, saving you time and money. Lastly changes are effective in seconds at the click of a button within the app. With the web app you can manage multiple user access and also view the history of any<br />license changes.</p>
<p><strong>How does it work?</strong><br />The app is integrated to StreamOne Cloud Marketplace via an API. You and your customers log in from a browser using active directory credentials. Once you login for the first time it will collate all your end user details and their CSP subscriptions and display them together on one screen. The Customer Portal SDK &ndash; Streamone is currently available as a Software Development Kit (SDK). Using Github Repository and Azure Resource Management (ARM) you can easily deploy it into your account. This application leverages the power of Azure for you to self-host it and easily maintain it after deployment.</p>
<p><strong>When will it be available?</strong><br />Customer Portal SDK &ndash; StreamOne is available now.</p>
<p><strong>Who will it be available to?</strong><br />It is available to all customers that have basic Azure knowledge and an Azure account.</p>
<p><strong>What does Customer Portal SDK - StreamOne look like?</strong><br />It looks great! It has a very simple and modern looking Graphical User Interface (GUI).</p>
<p><img src="https://github.com/techdata-cloudautomation/cpsso/blob/master/Marketing/7.JPG?raw=true" alt="" width="770" height="303" /></p>
<p><strong>What are the customer pre-requisites?</strong><br />&bull; Reseller agreement<br />&bull; Partner API credentials<br />&bull; Azure account<br />&bull; Microsoft account for login<br />&bull; Partner resources to maintain it on Azure (estimate monthly cost ~USD50.00)</p>
<p><strong>How to get onboarded?</strong><br />Contact SCM Customer Integrations Team according of your region:</p>
<p><strong>EUROPE</strong><br /><a href="mailto:EU.TDCloud@Techdata.com">EU.TDCloud@Techdata.com</a><br /><strong>US</strong><br /><a href="mailto:PartnerApi@Techdata.com">PartnerApi@Techdata.com</a><br /><strong>CANADA</strong><br /><a href="mailto:StreamOne@Techdata.ca">StreamOne@Techdata.ca</a></p>
<p><strong>Summary - Deployment using Github and ARM</strong><br />We have accomplished the automation of 90% of the deployment of this SDK application. Tech Data Business Partners with the most Azue knowledge will be able to self-deploy the app. The first step and last step are manual and the average time for the whole process is around 4 minutes;</p>
<ol>
<li>Manual - Application creation</li>
<li>Auto - Deployment</li>
<li>Manual - Update app site name selected during deployment</li>
</ol>
<p><strong>Auto Deployment Information</strong><br />The project utilizes GitHub public repository to host the application, as well as ARM (Azure Resource Management).&nbsp;It includes the "Deploy to Azure" button that allows you to launch the installation. It also contains files that perform the post-deployment actions that&nbsp; customize the database. The deployment includes the Azure templates, database scripts, the deployment code for the application, and support material. Azure copies all of the files from Github to the web app during the deployment process. For further details on how to deploy the application please read the file AutoDeploy. Customer Portal SDK StreamOne.</p>
<p><strong>IMPORTANT</strong><br />Before you proceed make sure you have read the Auto Deployment document and make sure you have all the necessary information for the input fields in the installation form:</p>
<p><img src="https://github.com/techdata-cloudautomation/cpsso/blob/master/Marketing/8.jpg?raw=true" alt="" width="645" height="772" /></p>
<ul>
<li><strong>Directory</strong>&nbsp;&ndash; This defaults to the Microsoft Azure Directory tied to the login account. If this isn&rsquo;t the correct directory, close all browser windows and start over at step #1.&nbsp;</li>
<li><strong>Subscription&nbsp;</strong>&ndash; This defaults to an Azure subscription tied to the login account. If needed, click the dropdown and select the desired subscription.</li>
<li><strong>Resource Group&nbsp;</strong>&ndash; This defaults to the&nbsp;<em>Create New</em>&nbsp;option to create a new resource group, which is recommended.</li>
<li><strong>Resource Group Name</strong>&nbsp;&ndash; If creating a new group, the page randomly generates a name (e.g., cpsso9fc2). Accept this value or change it to a custom value.&nbsp;</li>
<li><strong>Site Name</strong>&nbsp;&ndash; This will be the name for the instance of the&nbsp;<em>Customer Portal SDK StreamOne.</em>&nbsp;Accept the randomly generated name (e.g., cpsso9fc2), or enter a custom value. The name must be lowercase with no special characters and no spaces. Ensure whatever name entered is valid,&nbsp;<em>Name is available</em>&nbsp;will appear next to it if so.</li>
<li><strong>Site Location&nbsp;</strong>&ndash; Select a commercial Azure region to deploy the instance.</li>
<li><strong>SQL Password&nbsp;</strong>&ndash; Enter a password for the SQL Server and database deployed during the installation.</li>
<li><strong>Application Id&nbsp;</strong>&ndash; Copy/paste the application ID generated during the&nbsp;<em>Pre-Deployment Task</em></li>
<li><strong>Application Passcode&nbsp;</strong>&ndash; Copy/paste the application password generated during the&nbsp;<em>Pre-Deployment Task</em></li>
<li><strong>Allowed Resellers&nbsp;</strong>&ndash; Enter a comma-delimited list of allowed reseller email addresses that can use the&nbsp;<em>Customer Portal SDK StreamOne</em></li>
<li><strong>Client ID</strong>&nbsp;&ndash; Enter the partner API reseller client ID.</li>
<li><strong>Client Secret</strong>&nbsp;&ndash; Enter the partner API reseller secret.</li>
<li><strong>Reseller ID</strong>&nbsp;&ndash; Enter the StreamOne reseller ID.</li>
<li><strong>SOIN</strong>&nbsp;&ndash; Enter the partner API reseller SOIN value.</li>
<li><strong>Reseller Name</strong>&nbsp;&ndash; Enter the StreamOne reseller name.</li>
<li><strong>Notification Emails</strong>&nbsp;&ndash; Enter an email address that will receive notifications from the&nbsp;<em>Customer Portal SDK StreamOne&nbsp;</em>when end customers submit orders.</li>
<li><strong>Notification Email From</strong>&nbsp;&ndash; This is the sender address from which the application will send notifications.</li>
<li><strong>Notification Email Password</strong>&nbsp;&ndash; This is the password of the email account that will send the notifications.</li>
</ul>
<p>&nbsp;<img src="https://github.com/techdata-cloudautomation/cpsso/blob/master/Marketing/5.jpg?raw=true" alt="" width="500" height="333" /></p>
<p>&nbsp;<strong>Support Documents</strong></p>
<p>Support Documents are to be found in the root of the application</p>
<ul>
<li>Auto Deployment Technical Doc SDK Auto-deployment.pdf</li>
<li>User Manual Customer Portal SDK StreamOne Overview.pdf</li>
<li>Manual Deployment Technical Doc SDK Self Deployment.pdf</li>
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
<a href="https://azuredeploy.net/" target="_blank">
    <img src="http://azuredeploy.net/deploybutton.png"/>
</a>


