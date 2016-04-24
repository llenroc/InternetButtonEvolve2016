
$"Can you get a Xamarin to help reset {device}?" - Either internet toggling or device needs to be unclaimed.

Internet Button Mini Hack
===
Welcome to the Internet Button Hack. This simple hack will introduce you to Azure Event Bus which will allow you to easily receive messages from a Particle Internet Button or any lower level device. 

By completing this hack you will have a Azure Event Bus that will receive messages from the SimonSays game interactions. The mobile application will also publish events that can be received through the Event Bus.



Sounds amazing, doesn't it?  We think so too!  

We think you'll be able to complete this mini hack in 20 minutes.  If you get stuck or have any questions, no problem.  Head over to the Twilio booth and we'll be happy to walk through some code with you.

Alright.  With the intro out of the way, let’s get building!

### Mini Hack Requirements ###

Developers will need to setup a Particle account to interact with devices and setup the underlying framework for messages. The mobile application is already built with Xamarin.Forms and supports iOS, Android and UWP. If you would like to check out the source code, it will be made available in the near future. 

Getting Started
===
To get started you'll need to create an Azure account if you don't already have one. Head over to the [Azure Portal](https://portal.azure.com) to setup a [free trial](https://azure.microsoft.com/en-us/pricing/free-trial/). 

You will also need to create a Particle account. Don't worry, it's is completely free to [signup](https://build.particle.io/signup). You can also create an account through the mobile application if you don't want to interact with the Particle website. Once you have signed up, make sure to write down your username and password because we will need it soon. 

Next we need to install the Particle command line tools. We will use this to create the webhook that will send messages to our Azure Event Bus.  

[Particle CLI Windows Install Instructions](https://community.particle.io/t/tutorial-particle-cli-on-windows-07-jun-2015/3112)  
[Particle CLI Mac Install Instructions](https://github.com/spark/particle-cli#installing)

###Ready?

Azure account created? Check.  
Particle account created? Check.  
Particle CLI or Sample installed? Check.  

Let's get started then!

#The Mobile Application
As mentioned earlier, you can create a Particle account through the application:

<img src="https://raw.githubusercontent.com/michael-watson/InternetButtonEvolve2016/master/images/readme/SignUp.png?token=AIPtRrG7qxDNyUGyLJSTvDW_L90yZ0-vks5XJjNPwA%3D%3D" width="600"/>

Make sure you write down your username and password, you will need this to setup your webhook and login to the Particle cloud. Now you can login to the application.

<img src="https://raw.githubusercontent.com/michael-watson/InternetButtonEvolve2016/master/images/readme/Login.png?token=AIPtRnUyeKb8RXW2CFuc3JeKw9RRzjAuks5XJjZAwA%3D%3D" width="600"/>

Once logged into the application, you will land on the "Scan Device Page". This page allows us to scan the barcode on our InternetButton and take control of it.

<img src="https://raw.githubusercontent.com/michael-watson/InternetButtonEvolve2016/master/images/readme/ScanDevice.png?token=AIPtRumtOnoOOGOsZkBoYk1KLbj6yCL1ks5XJjaxwA%3D%3D" width="600"/>

We now control out Internet Button and we will land on the device mission control page.

<img src="https://raw.githubusercontent.com/michael-watson/InternetButtonEvolve2016/master/images/readme/SimonSays_Overview.png?token=AIPtRmVs71JpUPVRIKoUUkXfzgccASdAks5XJjcuwA%3D%3D" width="600"/>


Let's setup our Azure stuff before we go any further in the application.

#Azure Event Bus
Login to your Azure Portal or [start a free trial](https://azure.microsoft.com/en-us/pricing/free-trial/) if you don't have an Azure account. 

Once logged in to our Azure Portal, we are going to create a new Internet of Things resource called Azure Event Bus.

<img src="https://raw.githubusercontent.com/michael-watson/InternetButtonEvolve2016/master/images/azure/NewEventHub.png?token=AIPtRsemtRp6hR9cHxtfqALEjXPVDF0lks5XJjfAwA%3D%3D" width="600"/>

Some Internet of Things aren't supported by Azure IoT Hub because it requires the Azure IoT Library installed, but that doesn't mean we can't use Azure to understand our IoT devices better! Really every IoT device should speak to some hub to gather and analyze the data.

After selecting Event Hub, Azure should bring up everything to create a new Event Bus like below:

<img src="https://raw.githubusercontent.com/michael-watson/InternetButtonEvolve2016/master/images/azure/createNewEventHub.png?token=AIPtRkfklbXTyNCi5_LgtXj5vhGk3P_7ks5XJjo3wA%3D%3D" width="600"/>

Now once we have out Event Hub created, we will now need to configure the hub to have a token for our device webhook. We can do this by selecting "Configure" and adding a token.

#Creating the Webhook


Play the SimonSays game or any mobile interaction to send events to Azure. Show us your portal view with messages in the Event Bus to finish this mini-hack.

