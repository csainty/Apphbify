#AppHarbify

Instantly deploy and configure open source applications into the [AppHarbor](http://appharbor.com) cloud platform.

To see it in action, visit [appharbify.com](http://appharbify.com) and sign in with your AppHarbor account.

##Want to make your app available?
It's simple, fork AppHarbify, edit [Apps.json](https://github.com/csainty/Apphbify/blob/master/Apphbify/Apps.json) and send me a pull request.

Here is an example of what you need to add

```
{  
  'key': 'a unique key for your app - url safe',  
  'name': 'a friendly, short, name for your app',  
  'description': 'a longer description of your app',  
  'project_url': 'link to the project homepage',  
  'download_url': 'link to gzipped tarball of source to be deployed, github helpfully provides these',  
  'addons': ['an', 'array', 'of', 'add-ons', 'to', 'install'],  
  'variables': {
    'variable_name': 'variable description',
    'second_variable': 'description'  
    // A hash of variables that the user can configure. These are injected into web.config/appSettings
  },  
  'enableFileSystem': true/false  // Controls whether to enable file system access on AppHarbor  
}  
```

*For a list of supported add-ons see [Addons.json](https://github.com/csainty/Apphbify/blob/master/Apphbify/Addons.json)*

##Testing
To test your applications or site changes you will need to create an oAuth client at https://appharbor.com/clients and enter the client id and secret into your copy of `web.config`. You can also specify the callback url in `web.config` if you need to run on a specific port.

##Contact
Grab me on twitter [@csainty](http://twitter.com/csainty), follow my [blog](http://blog.csainty.com) or track me down somewhere on [JabbR](http://jabbr.net).
