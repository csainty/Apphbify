#Appharbify

Instantly deploy open source application into the [AppHarbor](http://appharbor.com) cloud platform.

To see it in action, visit http://appharbify.com and sign in with your AppHarbor account.

##Making your app available?
It's simple, fork Appharbify, edit [Apps.json](https://github.com/csainty/Apphbify/blob/master/Apphbify/Apps.json) and send me a pull request.

Here is an example of what you need to add

```
{  
  'key': 'a unique key for your app',  
  'name': 'a friendly, short, name for your app',  
  'description': 'a longer description of your app',  
  'project_url': 'link to the project homepage',  
  'download_url': 'link to gzipped tarball of source to be deployed, github helpfully provides these',  
  'addons': ['an', 'array', 'of', 'addons', 'to', 'install'],  
  'enableFileSystem': true/false  // Controls whether to enable file system access on AppHarbor  
}  
```

*For a list of supported addons see [Addons.cs](https://github.com/csainty/Apphbify/blob/master/Apphbify/Data/Addons.cs)*

##Contact
Grab me on twitter [@csainty](http://twitter.com/csainty), follow my [blog](http://blog.csainty.com) or track me down somewhere on [JabbR](http://jabbr.net).
