# The Intercept


> Bletchley Park, 1942. A component from the Bombe machine, used to decode intercepted German messages, has gone missing. One of the cryptographers is waiting to be interviewed, under direst suspicion. Is he stupid enough to have attempted treason? Or is he clever enough to get away?

*The Intercept* is a small demo game written in [ink](http://www.github.com/inkle/ink) and Unity that we built to demonstrate how to author a simple project. See how we like to structure our own ink files, and how easy it is to use the Unity plugin within a real game. We built *The Intercept* in a couple days for a game jam!

**[Download the game for Mac and Windows on our website](http://www.inklestudios.com/ink/theintercept)**

**[Read more about ink](http://www.inklestudios.com/ink)**

**[Read the The Intercept script](https://github.com/inkle/the-intercept/blob/master/Assets/Ink/TheIntercept.ink)**

## Getting Started

1. Clone the git repository. If you're unfamiliar with git, you may want to try using [GitHub Desktop][].

2. Open Unity and choose **Open** from the welcome dialog.

3. Find your repository's folder in the open dialog and choose **Select Folder**.

4. If you're presented with an **Opening Project in Non-Matching Editor Installation** dialog, no worries--just choose **Continue**:

   <img src="https://cloud.githubusercontent.com/assets/124687/14883846/c27c3b28-0d0e-11e6-82cb-213602597e0b.png">

5. After some time, you should see the Unity editor window. Locate the **Project** window at its bottom-left, expand the `Assets` folder if necessary, and select the `Scenes` subfolder inside it. Then double-click on the `Demo` scene that appears in the panel to the right of it:

   <img src="https://cloud.githubusercontent.com/assets/124687/15632705/a49af636-2568-11e6-84a9-d1992c9e2abf.png">

6. Play the game in your editor by clicking the play icon at the top of your editor window (or press Ctrl+P on Windows, Cmd+P on Mac).

7. When you're finished playing, press the play icon again to stop the game.

Now that you've got the hang of playing the game in the editor, you might want to tinker with the story itself.

### Tinkering With The Story

1. In the **Project** window, select the `Ink` subfolder and click on the `TheIntercept` icon in the panel on the right. Then locate the **Inspector** window on the right side of the Unity editor and click the **Open** button:

   <img src="https://cloud.githubusercontent.com/assets/124687/15632714/f4d8d708-2568-11e6-9558-9f898559d392.png">

   Once you click **Open**, Unity will open the story's ink script in a text editor.

3. Around line 75 of the ink script, you'll see the game's familiar starting text: `They are keeping me waiting`.  Change this to something else, like `I am a hamburger`:

   ```diff
    === start === 
 
    //  Intro
   -	- 	They are keeping me waiting. 
   +	- 	I am a hamburger. 
    		*	Hut 14[]. The door was locked after I sat down. 
    		I don't even have a pen to do any work. There's a copy of the morning's intercept in my pocket, but staring at the jumbled letters will only drive me mad. 
    		I am not a machine, whatever they say about me.
   ```

4. Save the file and switch back to Unity. You may briefly see a terminal window open and close itself in an instant: this is just [ink-Unity integration][]'s **Auto Compilation** functionality at work.

5. Play the game in the editor again; the story should now open with the text "I am a hamburger."

Hooray! Now you know how to tinker with the story, but it will quickly become annoying to have to wait through the whole introduction and play through part of the game just to interact with the part of the story you've changed.

### Using The Ink Player

Notice that underneath the **Open** button in the **Inspector** window is another button labeled **Play**. If you click it, the **Ink Player** window will appear:

<img src="https://cloud.githubusercontent.com/assets/124687/15632856/88a32940-256c-11e6-87f5-5d87a7557cdc.png">

This is a really quick way to try out changes to your story without needing to re-play the actual game. It also gives you greater visibility into the state of your story, such as the values of its [variables][], which can aid in debugging.

### Skipping The Intro

Perhaps you've tinkered with your story, but now you'd like to try it out in-game, just to make sure everything looks and feels just right. But you don't want to have to sit through the introduction every single time you do this.

Fear not! The creators of The Intercept also had this concern, and they implemented a handy way to skip the introduction entirely.

1. Locate the **Hierarchy** window on the left side of the Unity editor. Click the entry called `Main`.

2. On the right side of the Unity editor, the **Inspector** window will contain information about the `Main` object. Under the `Main (Script)` entry will be a checkbox titled `Skip Intro`:

   <img src="https://cloud.githubusercontent.com/assets/124687/15632943/cfb72d66-256e-11e6-9268-148e92cb4843.png">

Checking this and then playing the story will skip the introduction.

## License

**The Intercept** and **ink** are released under the MIT license. Although we don't require attribution, we'd love to know if you decide to use **ink** a project! Let us know on [Twitter](http://www.twitter.com/inkleStudios) or [by email](mailto:info@inklestudios.com).

[Newtonsoft's Json.NET](http://www.newtonsoft.com/json) is included, and also has the MIT License.

### The MIT License (MIT)
Copyright (c) 2016 inkle Ltd.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

[GitHub Desktop]: https://desktop.github.com/
[ink-Unity integration]: https://github.com/inkle/ink-unity-integration
[variables]: https://github.com/inkle/ink/blob/master/Documentation/WritingWithInk.md#part-3-variables-and-logic
