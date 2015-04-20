# Changing your Home Page Design #

It's actually possible to change the design of your Accudemia Home Page. You just have to sent your new design to ![http://bit.ly/gdaCCX?a=a.jpg](http://bit.ly/gdaCCX?a=a.jpg) and we will change it.

Also, it’s possible to stop using that page and have a better integration in your own site. You can put the login frame into your page; let’s see how to do that. You might want to contact the administrator of your website in order to do this, it will be a bit complex if you have never dealt with HTML code.

Now, the only thing you have to do is include the following code in your page’s source:

```
<iframe frameborder="0" height="80px" width="210px" scrolling="no"
src="https://<URL>/LoginForm.aspx?bgColor=%23<COLOR>&Layout=<LAYOUT>&Referer=<REFERER>">
</iframe>
```


There, you have to a few parts in that source:

| **Part** | **Description** | **Examples** |
|:---------|:----------------|:-------------|
| 

&lt;URL&gt;

 | Is your Account URL, this is the one you are using right now to access Accudemia. Always will end with accudemia.net. | collegename.accudemia.net |
| 

&lt;COLOR&gt;

 | Is the background color of the login form, specified in HTML format. You can use any design software to pick the color you like, or use this: http://html-color-codes.info/ | FFFFFF for white, FF0000 for red, ... |
| 

&lt;LAYOUT&gt;

 | Indicates how the login box will look like, can be either Vertical or Horizontal. | Vertical or Horizontal |
| 

&lt;REFERER&gt;

 | Is your personal college URL, Accudemia will redirect the users there when they leave the system. | http://www.collegehomepage.com |
| https | secure HTTP (HTTPS) is provided for the full site for an additional fee. Please contact your sales representative for more information | https |
| NoMask=1 | Add the NoMask parameter to the URL to disable the mask in the user ID input box | ...&NoMask=1 |


## Advanced Styles Customization ##

Also, you can specify a custom CSS location to use your own styles. It's intended for developers and advanced users.

To do so, add the `css` parameter in the URL, for example ([View Example](https://democollege.accudemia.net/LoginForm.aspx?css=/Styles/LoginForm.css)):
```
https://democollege.accudemia.net/LoginForm.aspx?css=/Styles/LoginForm.css
```