# Setting Up / More Information

Before you set any of this up, you will want to look through all
of the hidden slides in the power point slide deck included in [the
download for my session's talk](http://images.quicklearn.com/integrate2016/thegoods.zip).
They show each step along the way of the messages' flow so that you
can easily diagnose where things might be going sideways as you're
standing it all up.

Feel free to reach out to me at nickh AT quicklearn DOT com with any questions
you have about anything here.

I have **not** yet converted all of this into an ARM template sadly.
That is on my longer term TODO list so that I can more easily reuse
the demo myself. With the conference winding down at this point though,
I don't foresee getting around to that anytime soon.

Also it's a good exercise for understanding of the demo to deploy each
piece manually and think about what it is doing in the overall solution
(that's how I'm going to justify my lack of an ARM template -- we'll see
how long that excuse holds up).

## General Disclaimer ##
This is not production quality code. This is conference demo
quality code that served as my personal proof of a concept. There
are a lot of shortcuts taken (e.g., manual handling of http requests
to blob storage / caching of those requests rather than using the SDK -- 
so that the number of requests was absolutely minimized). 

Instead, use this code to understand what is potentially possible, and
then go build something better and laugh at how puny and weak mine looks
in comparison.

## Service Bus Setup

The service bus setup is pretty straight-forward. We start with a static
defined subscription with a single Sql filter rule that looks like the following:

```sql
[http://schemas.microsoft.com/BizTalk/2003/system-properties#MessageType] = 'http://schemas.quicklearn.com/correlation/demo#PrintJob'
```

If you want to keep the rest of the naming consistent with the demos,
name the topic **published** and the subscription **printjobprocess**.

## API Apps Setup
There are 2 API apps included as part of the solution. You will need
to deploy both of these:
* QuickLearn.ApiApps.Correlation
* QuickLearn.ApiApps.XmlPropertyPromotion

The first provides actions around creating/deleting instance subscriptions. The second looks up a blob container that contains BizTalk Server XML schemas
with notations about promoted properties, and then takes in an XML payload
from which to promote properties.

There is another API App hidden in the following project:
* QuickLearn.Demo.Bobble

This is also the website that is used to visualize the state of everything. The API App allows the application
state to be directly manipulated.

Undocumented features (that are now being documented):
* **/?r=1** - This route in the demo site will enable auto-refresh in the most barbaric but functional way
* **/Home/Reset** - This route in the demo site will flush the application state, but does not modify instance subscriptions
that might be created or stop any Logic Apps in flight -- be careful with this one.

## BizTalk Schemas Setup
The solution contains a BizTalk Server 2016 project named **QuickLearn.Demo.Messaging**. Inside
that project, you will find 2 document schemas and a property schema. The document
schemas need to be uploaded to a public blob container in Azure. If you don't want to deal with that
I have no plans of deleting mine, which you can find at **http://images.quicklearn.com/schemas**

## Logic Apps Setup
This is going to be the trickiest piece. Inside the solution, you will find a solution folder
that contains .json files for each of the 2 Logic Apps in my process. These files are not directly
usable however. They are copy/pasted directly from my runtime environment, but with secrets removed.

You can use these as guides to manually re-create your own. Do note that you will have to replace
my runtime URLs for my services with the URLs for the API Apps that you deployed instead. The same applies
for your Service Bus Connection string, and your blob container containing your BizTalk Server schemas.

# License Information
You can steal the code, as long as you make it better before production use.
As-is, it is **NOT** production quality. If used in production, it will destroy
your life and give you pox.

Don't steal the images. I paid to license the vectors over on [nounproject.com](http://www.nounproject.com).
If you want to use them, go over there and get yourself a $2 license so that the artists get some compensation for their work.
As for Dan Rosanova's bobble head, you'll have to ask him how he feels about your re-use of that art asset.
