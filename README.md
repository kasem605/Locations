# Locations
Locations app for IPhone

Xamarin based iPhone Application to show locations of interest

The app is developed mainly for the IPhone in C#

It consists of a List View page of locations that is filterable. Each column header can be clicked to sort ascending/descending in a toggle manner.

The details page can be accessed by clicking on each item in the list view page. This opens a detail page of the map that shows the location and the 
properties of each location.

A new entry page is included where I have a tableview datepicker that is rendered for the specific IOS platform A reverse value converter is included
to display a latency activity indicator. So when the user saves a new entry a delay is included to display the activity indicator to simulate the save mode. 
The save is just visual. Nothing is saved in the app as we have not yet built a persistent layer.
