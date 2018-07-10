# Customer Portal SDK StreamOne
<p><strong>What is &ldquo;Customer Portal SDK &ndash; StreamOne&rdquo;?</strong><br /> Customer Portal Software Development Kit (SDK) &ndash; StreamOne, previously known as My StreamOne, is a subscription management web application that allows both customers and end users to update their Microsoft CSP subscriptions.<br />There are two user levels:<br />&bull; The end user portal allows them to modify seat quantity and view log history<br />&bull; The customer portal allows you to manage users, modify seat quantity and view log history</p>
<p><strong>What are the key benefits?</strong><br />Firstly, it is very easy to use and there is no need to log into StreamOne. In StreamOne only administrators are able to manage users and modify seats. In Customer Portal SDK &ndash; StreamOne any approved users from your organization<br />can manage users and modify seats 24/7 on any device. Most importantly, your customers can also be given access to the app to modify seat quantity so you no longer need to be involved in the process, saving you time and money. Lastly changes are effective in seconds at the click of a button within the app. With the web app you can manage multiple user access and also view the history of any<br />license changes.</p>
<p><strong>How does it work?</strong><br />The app is integrated to StreamOne Cloud Marketplace via an API. You and your customers log in from a browser using active directory credentials. Once you login for the first time it will collate all your end user details and their CSP subscriptions and display them together on one screen. The Customer Portal SDK &ndash; Streamone is currently available as a Software Development Kit (SDK). Using Github Repository and Azure Resource Management (ARM) you can easily deploy it into your account. This application leverages the power of Azure for you to self-host it and easily maintain it after deployment.</p>
<p><strong>When will it be available?</strong><br />Customer Portal SDK &ndash; StreamOne is available now.</p>
<p><strong>Who will it be available to?</strong><br />It is available to all customers that have basic Azure knowledge<br />and an Azure account.</p>
<p><strong>What does Customer Portal SDK - StreamOne look like?</strong><br />It looks great! It has a very simple and modern looking Graphical User Interface (GUI).</p>
<p><strong>What are the customer pre-requisites?</strong><br />&bull; Reseller agreement<br />&bull; Partner API credentials<br />&bull; Azure account<br />&bull; Microsoft account for login<br />&bull; Partner resources to maintain it on Azure (estimate monthly cost ~USD50.00)</p>
<p><strong>How to get onboarded?</strong><br /> Contact SCM Customer Integrations Team depending of your region:</p>
<p><strong>EUROPE</strong><br /> <a href="mailto:EU.TDCloud@techdata.com">EU.TDCloud@techdata.com</a><br /> <strong>US</strong><br /> <a href="mailto:TDCloud@techdata.com">TDCloud@techdata.com</a><br /> <strong>CANADA</strong><br /> <a href="mailto:TDCloudCanada@techdata.ca">TDCloudCanada@techdata.ca</a></p>
<p><strong>Summary - Deployment using Github and ARM</strong><br /> We have accomplished the automation of 90% of the deployment of this SDK Application.With this even Tech Data Business Partners with basic knowledge on Azure will be able to self-deploy the App. Initial step and last step are manual and average time for the whole process is around 4 minutes.</p>
<p>1. Manual - Application Creation</p>
<p>2. Auto - Deployment</p>
<p>3. Manual - Update App Site Name selected during deployment</p>
<p><strong>Auto Deployment Information</strong><br /> Project utilizes GitHub Public repository to host the Application and ARM (Azure Resource Management).&nbsp;It includes the "Deploy to Azure" button below that allows you to launch the installation. It contains as well files to perform the post-deployment actions that will customize the database. The deployment includes the Azure templates, database scripts, the deployment code for the application, and support material. Azure copies all of the files from Github to the Web App during the deployment process. Further details on how to deploy the application please read the file AutoDeploy . Customer Portal SDK StreamOne.</p>
<p><strong>IMPORTANT</strong><br /> Before you proceed make sure you have read the Auto Deployment Documentation and make sure you have all the necessary information for the input fields in the installation form:</p>
<ul>
<li><strong>Directory</strong> &ndash; This defaults to the Microsoft Azure Directory tied to the login account. If this isn&rsquo;t the correct Directory, close all browser windows and start over at Step #1.&nbsp;</li>
<li><strong>Subscription </strong>&ndash; This defaults to an Azure subscription tied to the login account. If needed, click the dropdown and selected the desired subscription.</li>
<li><strong>Resource Group </strong>&ndash; This defaults to the <em>Create New</em> option to create a new resource group, which is recommended.</li>
<li><strong>Resource Group Name</strong> &ndash; If creating a new group, the page randomly generates a name (e.g., cpsso9fc2). Accept this value or change it to a custom value.&nbsp;</li>
<li><strong>Site Name</strong> &ndash; This will be the name for the instance of the <em>Customer Portal SDK StreamOne</em> Accept the randomly generated name (e.g., cpsso9fc2), or enter a custom value. The name must be lowercase with no special characters and no spaces. Ensure whatever name entered is valid, which will show <em>Name is available</em> if so.</li>
<li><strong>Site Location </strong>&ndash; Select a commercial Azure region to deploy the instance.</li>
<li><strong>SQL Password </strong>&ndash; Enter a password for the SQL Server and database deployed during the installation.</li>
<li><strong>Application Id </strong>&ndash; Copy/paste the Application Id generated during the <em>Pre-Deployment Task</em></li>
<li><strong>Application Passcode </strong>&ndash; Copy/paste the Application password generated during the <em>Pre-Deployment Task</em></li>
<li><strong>Allowed Resellers </strong>&ndash; Enter a comma-delimited list of allowed reseller email addresses that can use the <em>Customer Portal SDK StreamOne </em></li>
<li><strong>Client Id</strong> &ndash; Enter the partner API reseller client Id.</li>
<li><strong>Client Secret</strong> &ndash; Enter the partner API reseller secret.</li>
<li><strong>Reseller id</strong> &ndash; Enter the StreamOne reseller Id.</li>
<li><strong>SOIN</strong> &ndash; Enter the partner API reseller SOIN value.</li>
<li><strong>Reseller Name</strong> &ndash; Enter the StreamOne reseller name.</li>
<li><strong>Notification Emails</strong> &ndash; Enter an email address that will receive notifications from the <em>Customer Portal SDK StreamOne </em>when end customers submit orders.</li>
<li><strong>Notification Email From</strong> &ndash; This is the sender address from which the application will send notifications.</li>
<li><strong>Notification Email Password</strong> &ndash; This is the password of the email account that will send the notifications.</li>
</ul>
<p>&nbsp;</p>
<a href="https://azuredeploy.net/" target="_blank">
    <img src="http://azuredeploy.net/deploybutton.png"/>
</a>


