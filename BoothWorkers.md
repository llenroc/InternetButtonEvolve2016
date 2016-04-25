#General flow for mini-hack
1. Setup a Particle account
2. Use the provided mobile application to login and scan a device
	* Scanning the device with a logged in account will claim the device under that users account
3. Install Particle Command Line tools
4. Create an Azure free trial and login to Azure Portal
5. Create Azure Event Bus
6. Create Shared Access Key for a device with "Send" permissions
7. Edit json text to include Event bus url and Shared Access Key
8. Create Webhook using Particle CLI
9. Play game of Simon Says to receive messages

##Issues
Device refers to the Internet Button.

* Mobile app shows dialog that says: "Can you get a Xamarin to help reset {device}?"
	* Internet could be toggling on the device. If the device is flashing a green light, it is looking for internet signal.
		* If the device doesn't change to breathing blue light within 30 seconds, then you can try three things:
			1. Unplug the device, wait a few seconds and plug it back in.
			2. Switch the device out with a backup
			3. Call or slack Michael - (650)892-7190
		* If the device is breathing a blue light:
			* Find that device in the Particle dashboard and unclaim it from a user
			
<img src="https://raw.githubusercontent.com/michael-watson/InternetButtonEvolve2016/master/images/unclaimDevice.png?token=AIPtRrjsy9I7-Qu62k9zNTv4ua1wUA6Vks5XJnsGwA%3D%3D" width="600"/>

* Can't install Particle CLI tools for some reason
	* The MAC has command line tools setup and they can use it to set the webhook up. 
		* Run the following commands
			* particle logout
			* particle login
			* Have them login
			* There is a json file on the desktop they can modify and use to setup their webhook
			* They will have to manually enter the information, or could login to Azure portal directly on computer
			

