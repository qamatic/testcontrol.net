---
title: TestContro;.Net documentation
 
toc_footers:  
  - <a href='http://getmintleaf.org'>testcontrol.org</a>
  - <a href='https://github.com/qamatic/testcontrol.net'>Source @ github</a>  
 

includes:
  
search: true
---

# Introduction

TestControl is a light weight test automation framework for Windows Native, WinForm and WPF applications.

### Features
- A light weight framework - just few assembly DLLs required to get started
- Spy Tool which helps to recognize the objects on the screen
- Provides solid mechanism for identifying screen elements consistently in more reliable and in a resolution agnostic way
- Screen element locators are prone to developer changes(no more xpath!)
- In-Built smart wait logic for handling real time use cases
- Supports Windows Native, WinForm and WPF applications
- Supports Embedded Chrome Browser (CEFSharp) thru Selenium Extension
- Write your automation with your own choice of test runners like MSTest, NUnit, Cucumber SpecFlow or using Fitnesse
- It is customizable with by writing your own plugin extenders for example like generate your own test scripts in any language or detail inspection of a UI element on the screen in a customized way
 

##Installation

Download binaries and unzip to your favourite location or relative to your Visual Studio project
 
- [Download Release-binaries(x86/x64)-v3.zip](https://github.com/qamatic/testcontrol.net/raw/master/doc/www/files/TestControl-Rel-v3.zip) 

Note: Nuget distribution is not yet available at this time but eventually. 
 
## Why TestControl?

Well, the framework is being developed out of many hurdles and shortcomings with many other frameworks and tools available both open source and commerically. Most of the time, locating the control on the screen is the key for any successful automation but it has been a challenge.  So TestControl is trying to solve two main issues here: provide a precise way to locate a control on the screen and secondly be resilent to beat the failure!  Lastly, CEF browser automation which you will see in another section as how to automate using TestControl.
 
 
# Getting started

TestConntrol is a library is meant for people who have some .net developement skills because it does not offer record and replay mechanism. However it is not difficult for someone to work with very basic coding skills on C# or VB.  This framework written up around MS UIAutomation so highly recommended if any third party controls implement UIAutomation patterns in order to fully take control on accessing elements.   There are cases where paint events are drawing things on the screen and its simple imposible to capture those or to automate so that something you need to be aware of and will be discussed in advanced sections as how to overcome those.

## Write a simple test
 
 ```cs
     
    ApplicationUnderTest calcApp = new ApplicationUnderTest(@"C:\Windows\System32\", "calc.exe");
    
    //build your locators
    var repo = new ControlLocatorDefRepo("calcapp");
    repo.Wait(500);  // 
    repo.FindByName("Calculator", true); // find calculator by its name
    
    //now use locators to find
     var w = new Window();
     w.SystemUnderTestFromRepo("calcapp");
     Assert.AreEqual("Calculator", w.Caption);   
 ```
 
 So the above example is using repository concept if you would like to keep key map to store your locators for later use
 
 
# Locators

## Control Locator

    Control locators are used to find the right element on the target window in order to automate further.  Control locators are defined in a sequence of actions using following locator types.  Window elements are hierarchical by nature for example window being the root element and drop down being a child element like that.   So one can use multiple locator types to navigate to target element of interest.  

```cs
       //define as how to locate an element 
       var controldef = new ControlLocatorDef<FindControl>(
                                                    () => new FindWindow("Demo Form"),   // locate the window first
                                                    () => new FindByAutomationId("radioButton1") // and then locate the radio button using automation id
                                                   );
      
      //now choose or define your target element to be in order to access its values or trigger events on them 
      var radio = new RadioButtonControl();  // target element type
      radio.SystemUnderTest( controldef ); // set the locator def here
      radio.Selected = true; // now check radio button
                                                   
                                                   
```


##Find By Automation Id
    Automation id is generally an unique id given for the elements.  
    
```cs

    IFindControl findcontrol = FindByAutomationId("<automation id to locate a target element >");

```
    
##Find By Caption
```cs

    IFindControl findcontrol = FindByCaption("<caption/text to be used to locate a target element>");

```
  
##Find By Name
```cs

    IFindControl findcontrol = FindByName("<developers given name or OS based predefined names to be used to locate a target element>");

```
  
  
##Find Child By Caption
```cs

    IFindControl findcontrol = FindChildByCaption("<caption/text to be used to locate a target element from an root elementt>");

```
  
##Find Desktop Window
```cs

    IFindControl findcontrol = FindDesktopWindow();  //returns your desktop window as your target element

```
  
  
##Find Window
```cs

    IFindControl findcontrol = FindWindow(<Caption>, <className optional>);  //locating a target window element by its caption or class name or combination of both.

```
  
  
##Find Window By Mouse Position
```cs

    IFindControl findcontrol = FindWindowByMousePosition();  //locating a target window element by current mouse position.

```
  

##From Handle
```cs

    FromHandle(IntPtr handle);  //locating a target window element by HWND / window handle.

```
  
  
  
##Mouse Click
```cs

    MouseClick(Point pt);  //send mouse click on the given point position

```
  
  
##Move Mouse
```cs

    MouseClick(int moveToX, int moveToY);  //move mouse cursor position to desired x and y coordinates

```
  
##Right Click Mouse
```cs

    RightClickMouse();  //send right click on the current mouse cursor position

```
  
  
##Send Key Strokes
```cs

    SendKeyStrokes(keys);  //send key stroke, check microsoft documentation on SendKeys

```
    
##Wait
```cs

    Wait(int milliSec);  //waits for given time in milli sec 
    
    //for example
    
   var controldef = new ControlLocatorDef<FindControl>(
                                                () => new FindWindow("Demo Form"),   // locate the window first
                                                () => new Wait(1000),  //wait for 1 sec before proceed next step
                                                () => new FindByAutomationId("radioButton1") // and then locate the radio button using automation id
                                               );
                                                       

```
    
##Click Non-Window Control By Caption
```cs

 
    ClickNonWindowControlByCaption(String[] captionPath, bool dblClick = false, int offsetX = 2, int offsetY = 2);  

    //for example to work with context menu
            var cdef = new ControlLocatorDef<IFindControl>(() => new FindbyCaption("Demo application"),
                                                () => new RightClickMouse(),
                                                () => new Wait(100),
                                                () => new SendKeyStrokes("{DOWN}", true),
                                                () => new Wait(100),
                                                () => new FindWindowByMousePosition(),
                                                () => new Wait(100),
                                                () => new ClickNonWindowControlByCaption({"New", "Create folder"})  //under 'new' sub menu then click 'create folder' menu item
                );
            return cdef;    


```
    






# Standard Controls
 
 
 
# CEF Browser Control
 
 CEF is a real chrome browser embedded into a window control.  CEF control is becoming popular as organisations are moving towards web offerings and discontiue developements on desktops. As a result, desktop applications are embedded with web applications inside that makes automation complex because you need to have a seamless flow of automation between window controls on the applicatons and CEF control as a browser in order to manipulate its elements.  Rather reinvinting the wheel, TestControl uses Selenium drivers to work with but with modified version of chromedriver that can work along with TestControl.  So the modified version chromedriver.exe is named as TCChromeDriver.exe for our use.  
 
 
 ```cs
 
    // set your application under test
    IApplicationUnderTest app = new ApplicationUnderTest(@"..\TestApps\", "ceftest.exe"); //ceftest.exe is a winform cotinaing cef control
    
    //application main form has only a cef control browser control 
    //and your mainform window caption is set to "Google"
    
    app.RunOnce(); //now launch the application
    if (app.WaitForCaptionIfExists("Google", 20))  // wait for window appears with 20 retries
    {
        Assert.IsTrue(true);
    }
    else Assert.Fail("unable to get window caption");
    
    
      //locate the control as like any other .NET Control
    WebBrowserControl aBrowser = new WebBrowserControl("chrome"); //browser type to chrome
    aBrowser.SystemUnderTest(new ControlLocatorDef<FindControl>(
                                        () => new FindByAutomationId("Form1", true), //your main form id
                                        () => new FindByName("Chrome Legacy Window") //the spy shows the name as Chrome Legacy Window
            ));
    Assert.IsTrue(aBrowser.IsVisible);  //make sure your cef control is visible
    
  
    //** make sure, for this test formshow event of main form is set to load google.com into the cef browser             
    
    //now Testcontrol loads Selenium driver and switching to Selenium world
  
    // goto an Url
    var url = aBrowser.InternalDriver.GotoUrl("http://msn.com");
    Assert.AreEqual("http://msn.com", url);
    url = aBrowser.InternalDriver.GotoUrl("http://yahoo.com");
    Assert.AreEqual("http://yahoo.com", url);
    
    
    //to find an element
    aBrowser.InternalDriver.GotoUrl("http://google.com");
    var element = aBrowser.InternalDriver.FindChild("name:q"); // equivalent to ByName()
    Assert.IsNotNull(element);
    element.Text = "TDD"+ Enter;
    element = aBrowser.InternalDriver.FindChild("name:q");
    Assert.AreEqual("lst-ib", element.AsString("Attribute:id"));
                
                
    //here is an example if you want use direct selenium way
    aBrowser.GetWebDriver().Url = "http://www.littlewebhut.com/articles/html_iframe_example/";
                
    var frame = aBrowser.GetWebDriver().FindElement(By.Id("imgbox"));

    Assert.AreEqual("http://www.littlewebhut.com/images/eightball.gif", frame.GetAttribute("src"));

    //switch frame
    aBrowser.GetWebDriver().SwitchTo().Frame(frame);

    var img = aBrowser.GetWebDriver().FindElement(By.TagName("img"));
    Assert.AreEqual("http://www.littlewebhut.com/images/eightball.gif", img.GetAttribute("src"));                
    
 
 ```
 
 
 
 
 
 
 




 
 
  