# Enemy Template
****

## Welcome
****

Hello, I am Avien I am a Dev of the [OopsAllLemurians](https://thunderstore.io/package/OopsAllLemurians/) Team. Recently Mint was trying to find out a way with to make enemies in ROR2. It took about 3 days to get things working at a basic level. We do plan on improving upon this Template until it reaches all areas of what we find is needs in creating enemies (can be found under Plans for the Future below). I, Avien, will be maintaining the Enemy Template. I am in the Risk of Rain 2 Modding discord server, my name is Avien (OopsAllLemurians Team) in the server. **The setup guide is down below. Please read the License at the very bottom.**

## Recommendations
****

- Have experience with making skills, using Unity, Blender, and C#.
- Have used Rob's HeneryMod Template before (**Highly Recomemnded**).

## Features
****

- Can have multiple enemies in the same project
- Currently allows for spawning of normal enemies
- Have control over which maps enemies spawn in
- Have control over enemies’ stats
- Fast setup out of the gate
- Can handle multiple skinnedmeshrenderers

## Plans
****

- Add Logbook
- Add handling Bosses
- Add handling MiniBosses
- Add handling Elites?

## How it works/Setup
****

Most of what you need to do is similar to [HeneryMod Template](https://github.com/ArcPh1r3/HenryTutorial) with some differences. If things don't make sense check them out.

### Step 1, Setting up Unity

**First, Let’s install Unity Version**

For this project you will need Unity Version (2019.4.26f1). Here’s a link to the Unity Download Archive. [(Get Unity Version)](https://unity3d.com/get-unity/download/archive)

![](https://cdn.discordapp.com/attachments/568616591349252129/1004979686503354409/unknown.png)

Head to Unity 2019.X and scroll down until you see.

![](https://cdn.discordapp.com/attachments/568616591349252129/1004980287337418772/unknown.png)

I personally use Unity Hub but choose whichever one best works for you and download.

**Second, Lets open the Unity part of the project**

Download the latest Release version of this template above and open it.

![](https://cdn.discordapp.com/attachments/568616591349252129/1004987641416142849/unknown.png)

After selecting the Projects tab in Unity Hub click open and browse for the *EnemyTemplateUnityProject* folder.

![](https://cdn.discordapp.com/attachments/568616591349252129/1004989166527975474/unknown.png)

Then click on it to open.

**Third, Lets install the AssetBundle Browser/ Skip if you already have it on the right of your screen.**

Within the Unity Project head to window > Package Manager at the top and install *Asset Bundle Browser*.

![](https://cdn.discordapp.com/attachments/568616591349252129/1004978260565831720/unknown.png)

Next go to Window > AssetBundle Browser. I like to keep mine on the right side of my Unity Editor.

![](https://cdn.discordapp.com/attachments/568616591349252129/1004991570677858335/unknown.png)

**Fourth, Lets setup the AssetBundle Browser.**

Set the **Output Path** (The browse button) to the inside of *EnemyTemplateMod* folder.

![](https://cdn.discordapp.com/attachments/568616591349252129/1004987641416142849/unknown.png)

Next name your AssetBundle, select configure and right-click and rename the bundle. Mine is *gunpupassetbundle*. You can't use *myassetbundle* as its name.

![](https://cdn.discordapp.com/attachments/568616591349252129/1004993769696940072/unknown.png)

### Step 2, Setting up a model
**First, make sure your model is import ready / this is using Blender**

When your model is done and ready for **export** I recommend using these settings. This is what our team currently uses.
The type of file will be **.FBX**

![](https://cdn.discordapp.com/attachments/327602551518265344/1004998747475947520/unknown.png)
![](https://cdn.discordapp.com/attachments/327602551518265344/1004998766790713344/unknown.png)
![](https://cdn.discordapp.com/attachments/327602551518265344/1004998784096403506/unknown.png)
![](https://cdn.discordapp.com/attachments/327602551518265344/1004998802802999306/unknown.png)

**Second, importing the model and its parts**

Now head back to the Unity Project. Drag'n'drop the **FBX** of the model into the **Project window** at the bottom. I recommend making a new folder for every enemy you make for easier modifying in the future (just like the *Gunpuppy* folder below).

![](https://cdn.discordapp.com/attachments/568616591349252129/1005000224038735952/unknown.png)

Now import all the other parts of your model for example textures.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005003242289250334/unknown.png)

Next make a material for the texture. Just **Right-click** the empty space in the **Project window** and find **Create > Material**. Name the material what you want, then drag the texture into the Albedo slot (go to the inspector window at the upper right). If there are transparencies set the **Shader** to **Fake RoR/Hopoo Games/Deferred/HGStandard** and **check Cutout**. Emissive materials work with the standard Unity  shader.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005010947007578203/unknown.png)

Now click on your imported enemy model and head to the inspector on the right. Click the **Materials** tab and drag'n'drop the material into the slot under **Remapped Materials**. Then hit **Apply**.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005014389151236137/unknown.png)

Great now we have a model with its textures applied.

**Third, setup the prefab gameobjects**

Okay now let’s drag'n'drop the model into the **Hierarchy**.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005016210280304650/unknown.png)

Now we add two empty gameobjects to the model. Right-click the core model, mine is *GunPuppy* and click **create empty**. Now rename them to **MainHurtbox and HealthBarOrigin**. Next move **HeathBarOrigin** above the models head using the green arrow after selecting **HeathBarOrigin**. **Add a capsule collider to the MainHurtbox.**

![](https://cdn.discordapp.com/attachments/568616591349252129/1005020647946207292/unknown.png)

**Fourth, setup the prefab ChildLocator**

Alright now for the setting up the child locator. Click on the core model (in the upper left) and in the inspector hit add component **ChildLocator**.set the size to 3. Add the same names I do to the list.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005026501009354822/unknown.png)

Now Drag'n'drop the gameobject *MainHurtbox* into **MainHurtbox** above. Repeat for *HealthBarOrigin* and drag the game object that hold your skinnedmeshrenderer into the **Mesh**. My skinnedmesh renderer was found in the *gunpuppy.model-Skin-Mesh[0]*.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005033124192854056/unknown.png)

This is what it should look like now.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005033585264296007/unknown.png)

**Fifth, setup the prefab Animations**

Head back to the core model and add component **Animator**. I would recommend copying **Henry's AnimationController** and building from it if your new to understanding **AnimationControllers** as well as watching some videos on how to use them in Unity. I can't explain much about this part because it really depends on what you want to do. But if you want to just see if your enemy will just spawn first I suggest you take *gunpuppies* AnimationController and use it. this is what our example looks like for the **Animator**.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005038666466471966/unknown.png)

**Sixth, save as a prefab**

Drag'n'drop the core model in the **Hierarchy** to the Project window at the bottom. A window will pop-up, you want to hit **Original Prefab**. Now rename it to the name you want to give it but you **must include "enm"** in the name. For example mine is **enmGunPupBasic**.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005040815640412190/unknown.png)

Now the prefab is done.

### Step 3, Build the AssetBundle
**I recommend doing this every time you edit the Unity Project.**

Head to the right-side window and go to **AssetBundles** and the Configure Tab. Hit the Refresh button to the left of the Configure tab.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005042182459248651/unknown.png)

Now hit the Build tab button and hit **Build**.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005042555932639324/unknown.png)

So easy, I love it.

### Step 4, Telling AssetBundle to be loaded
Head back to the folder.

![](https://cdn.discordapp.com/attachments/568616591349252129/1004987641416142849/unknown.png)

Now Launch the **EnemyTemplateMod.sln** you will need Visual Studio. If you don't have it you can get it here [VisualStudio2022](https://visualstudio.microsoft.com/vs/), you will need to select the **community version** for the free one. **NOTE, you do not need the latest Version of Visual Studio to use this**.
Now that we are in the code side of the project lets finish the **AssetBundle**. Off to the right of your visual studio you will see a **Solution Explorer** window.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005045413478416494/unknown.png)

Within this window you should see your **AssetBundle**. Mine is named ***gunpupassetbundle*** just like it was in the Unity Editor. Now click on it, and you should see a window under it called **Properties**. Then Change **BuildAction** to **Embedded resource**. **Delete the gunpupassetbundle very important.**

![](https://cdn.discordapp.com/attachments/568616591349252129/1005046198161391626/unknown.png)

Now that it's done you will be able to just repeat **Step 3** every time you update something in the Unity side of the project.

### Step 5, Setting up your enemy in the code/ This is the big part
We will only be interacting with 4 scripts. This will cover the enemies: Spawning, Death, Stats, AiSkillsDriver, and Skills. Most things are explained within the scripts.

**First, let’s update the plugin script**
In the **EnemyTemplateMod.sln's** Solution Explorer go to **EnemyPlugin.cs**.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005245331560210534/unknown.png)

So now let’s change this to your own. My **MODUID** is *com.Lemurians.EnemyTemplateMod*. Now make it **com.(your author name).(mod name)**. Next change **MODNAME** to your modname. Last set **DEVELOPER_PREFIX** to your (author name/**Must be all CAPS**). Then lastly the **assetbundle name**.

**Second, lets update the enemy script**
In the **Solution Explorer** head to **Modules > Enemies > GunPupEnemy**. This Enemies folder is where you will put all your enemies.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005248107438690376/unknown.png)

Everything within this file is explained in the code. **Duplicate this file if you want to add more enemies**.
**For more information:**
- Stats found on line 46
- Meshrenderer found on line 81
- SpawnCard found on line 103
- EnemySpawning found on line 127
- AISkillDrivers found on line 138
- Skills found on line 187 (**Must have 4 skills, no less or it breaks**)

**Third, lets update the enemyspawn script**
In the **Solution Explorer** head to **Modules > EnemiesSpawn > GunPupSpawn**. This EnemiesSpawn folder is where you will put all your enemies’ spawns. It handles vfx for when your enemy spawns as well as adding a special animation that they can use. 

![](https://cdn.discordapp.com/attachments/568616591349252129/1005253928637702164/unknown.png)

To add the animation put it in **public override void OnEnter()**. The **effecPrefab** lets you copy vfx from pre-existing enemies.  **Duplicate this file if you want to add more enemies (plug in to your enemy script on line 100)**.

**Fourth, lets update the enemydeath script**
In the **Solution Explorer** head to **Modules > EnemiesDeath > GunPupDeath**. This EnemiesDeath folder is where you will put all your enemies’ deaths. It handles: vfx for when your enemy death, adding a special animation that they can use, and how money and experience you earn. **Duplicate this file if you want to add more enemies (plug in to your enemy script on line 97)**.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005256649428193380/unknown.png)

**Final part, lets build and test the mod**
Right-click on **EnemyTemplateMod** the one in white **Bold**. Then hit **Properties** and click Build on the left of your screen. 

![](https://cdn.discordapp.com/attachments/568616591349252129/1005045413478416494/unknown.png)

This is what the 2022 version looks like. Set the output path your ThunderstoreModManager Profiles Plugins, like my path below.

![](https://cdn.discordapp.com/attachments/568616591349252129/1005281908902609056/unknown.png)

Then hit (**ctrl +  b**) to build and done. It will apper as a folder called **Debug**. Its done now run the game.

### Conclusion
****
That was a lot to go through. Refer back to this whenever you need. I am in college but I’m open to questions and suggestion on things that you may have concerning the EnemyTemplate. Mint is also open to questions; He's been working on a mod using a beta version of the template already, so he has better answers. We are still new to this whole Modding thing and have made 3 and soon to be 4 mods. Only 1 was published so far due to passing out quality check for our mods. There should be 2 more mods on the way. Please don't ask about the unpublished mods or when they will be released. You will see them in the **Showcase** channel when they are completed.

## Other works
****

- [Team Page](https://thunderstore.io/package/OopsAllLemurians/)
- [Tyto the Swift](https://thunderstore.io/package/OopsAllLemurians/Tyto_the_Swift/)

## Credit

****
#### Creators
- Avien (OopsAllLemurians)
- Mint (OopsAllLemurians)

#### Refrences
- HenryMod Template (Rob and Timesweeper)
- RelicMod (nayDPz)
- ClayMen and ArchaicWisp (Moffein)

## License
****

**This is what I ask of people who want to use this Template.**
- **Do not re-upload this without modifying it.**
- **Give credit to use if you use our Template.**
- **You can modify this Template as much as you want and use it for whatever you want (for example if you wanted to make a mod that’s NSFW with this, go for it).**
- **Have fun, let’s make Enemy Mods as big as Playable Characters.**

****

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [dill]: <https://github.com/joemccann/dillinger>
   [git-repo-url]: <https://github.com/joemccann/dillinger.git>
   [john gruber]: <http://daringfireball.net>
   [df1]: <http://daringfireball.net/projects/markdown/>
   [markdown-it]: <https://github.com/markdown-it/markdown-it>
   [Ace Editor]: <http://ace.ajax.org>
   [node.js]: <http://nodejs.org>
   [Twitter Bootstrap]: <http://twitter.github.com/bootstrap/>
   [jQuery]: <http://jquery.com>
   [@tjholowaychuk]: <http://twitter.com/tjholowaychuk>
   [express]: <http://expressjs.com>
   [AngularJS]: <http://angularjs.org>
   [Gulp]: <http://gulpjs.com>

   [PlDb]: <https://github.com/joemccann/dillinger/tree/master/plugins/dropbox/README.md>
   [PlGh]: <https://github.com/joemccann/dillinger/tree/master/plugins/github/README.md>
   [PlGd]: <https://github.com/joemccann/dillinger/tree/master/plugins/googledrive/README.md>
   [PlOd]: <https://github.com/joemccann/dillinger/tree/master/plugins/onedrive/README.md>
   [PlMe]: <https://github.com/joemccann/dillinger/tree/master/plugins/medium/README.md>
   [PlGa]: <https://github.com/RahulHP/dillinger/blob/master/plugins/googleanalytics/README.md>
