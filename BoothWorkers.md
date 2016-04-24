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
			
<img src="https://raw.githubusercontent.com/michael-watson/InternetButtonEvolve2016/master/images/readme/SimonSays_Overview.png?token=AIPtRmVs71JpUPVRIKoUUkXfzgccASdAks5XJjcuwA%3D%3D" width="600"/>

* 
$"Can you get a Xamarin to help reset {device}?" - Either internet toggling or device needs to be unclaimed.