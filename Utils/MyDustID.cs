using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 命名空间自己改，这个文件是四十九落星内部源码的一部分
/// 使用的时候需要 using FallenStar49;
/// </summary>
namespace TemplateMod.Utils
{
	/// <summary>
	/// DXTsT自制的粒子ID表
	/// 制作时间：2017/1/31
	/// 版权所有：DXTsT & 四十九落星制作组
	/// 
	/// 说明：以下字段带有（！）标识符的说明此粒子效果会在黑暗中自发光
	/// 带有（.）标识符说明此粒子效果会高亮显示但是不会发光
	/// 其余Dust全部都不会发光！
	/// </summary>
	public static class MyDustId
	{
		/// <summary>
		/// 土壤粒子，不发光，受重力影响。
		/// </summary>
		public const int BrownDirt = 0;
		/// <summary>
		/// 岩石粒子，不发光，受重力影响。
		/// </summary>
		public const int GreyStone = 1;
		/// <summary>
		/// 浅绿色粒子，不发光，受重力影响。
		/// </summary>
		public const int GreenGrass = 2;
		/// <summary>
		/// 绿色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinGreenGrass = 3;
		/// <summary>
		/// 灰色粒子，不发光，受重力影响。
		/// </summary>
		public const int GreyPebble = 4;
		/// <summary>
		/// 深红色粒子，不发光，受重力影响。
		/// </summary>
		public const int RedBlood = 5;
		/// <summary>
		/// (!)橘黄色火焰粒子，自发光，范围中等，受重力影响。
		/// </summary>
		public const int Fire = 6;
		/// <summary>
		/// 深土壤色粒子，不发光，受重力影响。
		/// </summary>
		public const int Wood = 7;
		/// <summary>
		/// 铁矿色粒子，不发光，受重力影响。
		/// </summary>
		public const int PurpleGems = 8;
		/// <summary>
		/// 铜矿色粒子，不发光，受重力影响。
		/// </summary>
		public const int OrangeGems = 9;
		/// <summary>
		/// 金矿色粒子，不发光，受重力影响。
		/// </summary>
		public const int YellowGems = 10;
		/// <summary>
		/// 银矿色粒子，不发光，受重力影响。
		/// </summary>
		public const int WhiteGems = 11;
		/// <summary>
		/// 精金矿色粒子，不发光，受重力影响。
		/// </summary>
		public const int RedGems = 12;
		/// <summary>
		/// 钴蓝矿色粒子，不发光，受重力影响。
		/// </summary>
		public const int CyanGems = 13;
		/// <summary>
		/// 魔晶矿色粒子，不发光，受重力影响。
		/// </summary>
		public const int CorruptionParticle = 14;
		/// <summary>
		/// （!）冰晶色粒子，自发光，范围大，不受重力影响且在重力下持续时间变长。
		/// </summary>
		public const int BlueMagic = 15;
		/// <summary>
		/// （.）浅蓝云色粒子，不发光，不受重力影响且在重力下持续时间变长。
		/// </summary>
		public const int WhiteClouds = 16;
		/// <summary>
		/// 蓝黑色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinGrey = 17;
		/// <summary>
		/// 叶绿色粒子，不发光，受重力影响。
		/// </summary>
		public const int SicklyGreen = 18;
		/// <summary>
		/// 金色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinYellow = 19;
		/// <summary>
		/// （!）纯白色粒子，自发光，范围中等，不受重力影响且在重力下持续时间变长。
		/// </summary>
		public const int WhiteLingering = 20;
		/// <summary>
		/// （!）亮粉色粒子，自发光，范围小，不受重力影响且在重力下持续时间变长。
		/// </summary>
		public const int PurpleLingering = 21;
		/// <summary>
		/// 深土壤色粒子，不发光，受重力影响。
		/// </summary>
		public const int Brown = 22;
		/// <summary>
		/// 微粉土壤色粒子，不发光，受重力影响。
		/// </summary>
		public const int Orange = 23;
		/// <summary>
		/// 微紫土壤色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinBrown = 24;
		/// <summary>
		/// 狱岩石色粒子，不发光，受重力影响。
		/// </summary>
		public const int Copper = 25;
		/// <summary>
		/// 枯草色粒子，不发光，受重力影响。
		/// </summary>
		public const int Iron = 26;
		/// <summary>
		/// （!）蓝紫粉色粒子，自发光，范围中等，不受重力影响且在重力下持续时间变长。
		/// </summary>
		public const int PurpleLight = 27;
		/// <summary>
		/// 深铜色粒子，不发光，受重力影响。
		/// </summary>
		public const int DullCopper = 28;
		/// <summary>
		/// （!）深蓝色粒子，自发光，受重力影响。
		/// </summary>
		public const int DarkBluePinkLight = 29;
		/// <summary>
		/// 银白色粒子，不发光，受重力影响。
		/// </summary>
		public const int Silver = 30;
		/// <summary>
		/// 白云色粒子，不发光，不受重力影响。
		/// </summary>
		public const int Smoke = 31;
		/// <summary>
		/// 深黄色粒子，不发光，受重力影响。
		/// </summary>
		public const int Sand = 32;
		/// <summary>
		/// 水蓝色粒子，不发光，受重力影响。
		/// </summary>
		public const int Water = 33;
		/// <summary>
		/// 金黄火焰色粒子，自发光，范围中等，在重力下不显现，在无重力下显现。
		/// </summary>
		public const int RedLight = 35;
		/// <summary>
		/// 浅黑色粒子，不发光，受重力影响。
		/// </summary>
		public const int MuddyPale = 36;
		/// <summary>
		/// 深蓝黑色粒子，不发光，受重力影响。
		/// </summary>
		public const int DarkGrey = 37;
		/// <summary>
		/// 深土壤色粒子，不发光，受重力影响。
		/// </summary>
		public const int MuddyBrown = 38;
		/// <summary>
		/// 绿色粒子，不发光，受重力影响。
		/// </summary>
		public const int JungleGrass = 39;
		/// <summary>
		/// 深叶绿色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinGrass = 40;
		/// <summary>
		/// （!）亮水蓝色粒子，自发光，范围大，在重力下停留扩散且时间较长，无重力时消散较快。
		/// </summary>
		public const int BlueCircle = 41;
		/// <summary>
		/// 深钴蓝色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinTeal = 42;
		/// <summary>
		/// （!）亮光色粒子，不稳定发光，光源不穿透，光照范围与大小成正比，不受重力影响。
		/// </summary>
		public const int WhiteLight = 43;
		/// <summary>
		/// （!）黄绿色粒子，发白光，范围很大，在重力下停留扩散且时间较长，无重力时消散较快。
		/// </summary>
		public const int GreenSpores = 44;
		/// <summary>
		/// （!）深水蓝色粒子，自发光，在重力下停留扩散且时间较长，无重力时消散较快。
		/// </summary>
		public const int LightBlueCircle = 45;
		/// <summary>
		/// 深绿色粒子，不发光，不受重力影响。
		/// </summary>
		public const int GreenMaterial = 46;
		/// <summary>
		/// X深蓝色粒子，不发光，受重力影响。
		/// </summary>
		public const int CyanGrass = 47;
		/// <summary>
		/// X蘑菇矿色粒子，不发光，受重力影响。
		/// </summary>
		public const int BlueMushroom = 48;
		/// <summary>
		/// X蓝偏黑色粒子，不发光，受重力影响。
		/// </summary>
		public const int BlueDrakParticle = 49;
		/// <summary>
		/// X深精金矿色粒子，不发光，受重力影响。
		/// </summary>
		public const int RedParticle = 50;
		/// <summary>
		/// 白土色粒子，不发光，受重力影响。
		/// </summary>
		public const int PearlStone = 51;
		/// <summary>
		/// 粉色水 不发浅粉色 受重力 高度透明
		/// </summary>
		public const int PinkWater = 52;
		/// <summary>
		/// 灰色材质 不发浅深灰色 受重力 不受重力消失快  
		/// </summary>
		public const int GreyMaterial = 53;
		/// <summary>
		/// 黑色材质 不发浅黑色 受重力 不受重力消失快  
		/// </summary>
		public const int BlackMaterial = 54;
		/// <summary>
		/// （!）亮金火焰色粒子，自发光，范围较大，受重力时旋转扩散 
		/// </summary>
		public const int OrangeFx = 55;
		/// <summary>
		/// （!）天蓝色粒子，自发光，范围中等，在重力下扩散，消散较快。
		/// </summary>
		public const int CyanFx = 56;
		/// <summary>
		/// （!）小型黄色神圣特效，发黄浅浅黄色、金色，受重力时旋转扩散   
		/// </summary>
		public const int YellowHallowFx = 57;
		/// <summary>
		/// （!）亮粉白色粒子，自发光，范围较大，不受重力影响。
		/// </summary>
		public const int PinkMagic = 58;
		/// <summary>
		/// （!）晶蓝色粒子，高蓝光，受重力影响。
		/// </summary>
		public const int BlueTorch = 59;
		/// <summary>
		/// （!）偏粉红色粒子，高红光，受重力影响。
		/// </summary>
		public const int RedTorch = 60;
		/// <summary>
		/// （!）亮绿色粒子，高绿光，受重力影响。
		/// </summary>
		public const int GreenTorch = 61;
		/// <summary>
		/// （!）紫色粒子，高紫光，受重力影响。
		/// </summary>
		public const int PurpleTorch = 62;
		/// <summary>
		/// (!)灰白色粒子，白光，受重力影响。
		/// </summary>
		public const int WhiteTorch = 63;
		/// <summary>
		/// (!)纯金色粒子，自发光，受重力影响。
		/// </summary>
		public const int YellowTorch = 64;
		/// <summary>
		/// (!)深紫色粒子，自发光，受重力影响。
		/// </summary>
		public const int DemonTorch = 65;
		/// <summary>
		/// (!)白色粒子，自发光，范围非常大，在重力下迅速变大并且旋转，无重力时消散较快。
		/// </summary>
		public const int WhiteTransparent = 66;
		/// <summary>
		/// (!)浅海蓝色粒子，自发光，范围中等，受重力影响。
		/// </summary>
		public const int CyanIce = 67;
		/// <summary>
		/// (!)暗蓝冰晶，受重力时发暗蓝光深蓝色，不受重力时高亮发蓝光亮青色 
		/// </summary>
		public const int DarkCyanIce = 68;
		/// <summary>
		/// 粉色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinPink = 69;
		/// <summary>
		/// (!)透明紫色粒子 受重力时发暗紫光深紫色 不受重力时高亮发粉光亮粉色
		/// </summary>
		public const int TransparentPurple = 70;
		/// <summary>
		/// (.)透明粉色特效 发粉浅亮粉色 受重力时旋转扩散
		/// </summary>
		public const int TransparentPinkFx = 71;
		/// <summary>
		/// (.)红粉色粒子，高亮，在重力下扩散并消散，无重力时消散较快。
		/// </summary>
		public const int SolidPinkFx = 72;
		/// <summary>
		/// (!)亮红粉色粒子，自发光，范围中等，在重力下扩散并消散，无重力时消散较快。
		/// </summary>
		public const int BrightPinkFx = 73;
		/// <summary>
		/// (!)亮绿色粒子，自发光，范围中等，在重力下扩散并消散，无重力时消散较快。
		/// </summary>
		public const int BrightGreenFx = 74;
		/// <summary>
		/// (!)诅咒火把，发黄绿浅黄绿色，受重力
		/// </summary>
		public const int CursedFire = 75;
		/// <summary>
		/// (.)下雪，不发浅白色，受重力时旋转大范围扩散，存在时间长，遇到物块消失
		/// </summary>
		public const int Snow = 76;
		/// <summary>
		/// 阴影木 不发浅深灰色 受重力 不受重力消失快
		/// </summary>
		public const int ThinGrey1 = 77;
		/// <summary>
		/// 红木 不发浅红棕色 受重力 不受重力消失快
		/// </summary>
		public const int ThinCopper = 78;
		/// <summary>
		/// 薄黄材质 不发浅浅黄色 受重力下落不旋转 不受重力消失快
		/// </summary>
		public const int ThinYellow1 = 79;
		/// <summary>
		/// 冰块 不发浅蓝白色 受重力 不受重力消失快。
		/// </summary>
		public const int IceBlock = 80;
		/// <summary>
		/// 锡矿石 不发浅灰色 受重力 不受重力消失快
		/// </summary>
		public const int Tin = 81;
		/// <summary>
		/// 铅矿石 不发浅蓝黑色 受重力 不受重力消失快
		/// </summary>
		public const int Lead = 82;
		/// <summary>
		/// 铅矿石 不发浅蓝黑色 受重力 不受重力消失快
		/// </summary>
		public const int Tungsten = 83;
		/// <summary>
		/// 铂矿石 不发浅浅蓝银色 受重力 不受重力消失快
		/// </summary>
		public const int Platinum = 84;
		/// <summary>
		/// 沙褐材质 不发浅浅橙色 受重力 不受重力消失快
		/// </summary>
		public const int ThinSandy = 85;
		/// <summary>
		/// (!)少女粉色粒子，自发光，范围较大，受重力影响。
		/// </summary>
		public const int PinkTrans = 86;
		/// <summary>
		/// (!)亮金黄色粒子，自发光，范围中等，受重力影响。
		/// </summary>
		public const int YellowTrans = 87;
		/// <summary>
		/// (!)白偏浅蓝色粒子，自发光，范围中等，受重力影响。
		/// </summary>
		public const int BlueTrans = 88;
		/// <summary>
		/// (!)白绿色粒子，自发光，范围中等，受重力影响。
		/// </summary>
		public const int GreenTrans = 89;
		/// <summary>
		/// (!)粉红色粒子，自发光，范围中等，受重力影响。
		/// </summary>
		public const int RedTrans = 90;
		/// <summary>
		/// (!)亮白色粒子，自发光，范围大，受重力影响。
		/// </summary>
		public const int WhiteTrans = 91;
		/// <summary>
		/// (!)蓝白色粒子，自发光，范围较大，受重力影响。
		/// </summary>
		public const int CyanTrans = 92;
		/// <summary>
		/// 松绿色粒子，不发光，受重力影响。
		/// </summary>
		public const int DarkGrass = 93;
		/// <summary>
		/// 深黄绿色粒子，不发光，受重力影响。
		/// </summary>
		public const int PaleDarkGrass = 94;
		/// <summary>
		/// 深红偏黑色粒子，不发光，受重力影响。
		/// </summary>
		public const int DarkRedGrass = 95;
		/// <summary>
		/// 深蓝色粒子，不发光，受重力影响。
		/// </summary>
		public const int BlackGreenGrass = 96;
		/// <summary>
		/// 深紫偏黑色粒子，不发光，受重力影响。
		/// </summary>
		public const int DarkRedGrass1 = 97;
		/// <summary>
		/// 紫色粒子，不发光，受重力影响。
		/// </summary>
		public const int PurpleWater = 98;
		/// <summary>
		/// 浅绿色粒子，不发光，受重力影响。
		/// </summary>
		public const int CyanWater = 99;
		/// <summary>
		/// 浅粉色粒子，不发光，受重力影响。
		/// </summary>
		public const int PinkWater1 = 100;
		/// <summary>
		/// 浅蓝色粒子，不发光，受重力影响。
		/// </summary>
		public const int CyanWater1 = 101;
		/// <summary>
		/// 浅黄土色粒子，不发光，受重力影响。
		/// </summary>
		public const int OrangeWater = 102;
		/// <summary>
		/// 深蓝偏白色粒子，不发光，受重力影响。
		/// </summary>
		public const int DarkBlueWater = 103;
		/// <summary>
		/// 深粉偏黑色粒子，不发光，受重力影响。
		/// </summary>
		public const int HotPinkWater = 104;
		/// <summary>
		/// 大红色粒子，不发光，受重力影响。
		/// </summary>
		public const int RedWater = 105;
		/// <summary>
		/// (.)红黄绿三色火焰色粒子，高亮，受重力影响。
		/// </summary>
		public const int RgbMaterial = 106;
		/// <summary>
		/// (!)亮白绿色粒子，自发光，范围小，在重力下能够悬停较长时间。
		/// </summary>
		public const int GreenFXPowder = 107;
		/// <summary>
		/// 浅灰浅蓝色粒子，不发光，受重力影响。
		/// </summary>
		public const int PurpleRound = 108;
		/// <summary>
		/// 黑色粒子，不发光，受重力影响。
		/// </summary>
		public const int BlackMaterial1 = 109;
		/// <summary>
		/// (.)亮浅蓝偏绿色粒子，高亮，在重力下快速扩散范围但不大。
		/// </summary>
		public const int GreenBubble = 110;
		/// <summary>
		/// (.)亮蓝色粒子，高亮，在重力下快速扩散范围但不大。
		/// </summary>
		public const int CyanBubble = 111;
		/// <summary>
		/// (.)亮粉色粒子，高亮，在重力下快速扩散范围但不大。
		/// </summary>
		public const int PinkBubble = 112;
		/// <summary>
		/// (.)亮深蓝偏白色粒子，高亮，在重力下快速扩散范围但不大。
		/// </summary>
		public const int BlueIce = 113;
		/// <summary>
		/// (.)亮粉偏红色粒子，高亮，在重力下快速扩散范围但不大。
		/// </summary>
		public const int PinkYellowBubble = 114;
		/// <summary>
		/// 锈红色粒子，不发光，受重力影响
		/// </summary>
		public const int RedGrass = 115;
		/// <summary>
		/// 深蓝色粒子，不发光，受重力影响。
		/// </summary>
		public const int BlueGreenGrass = 116;
		/// <summary>
		/// 较锈红色粒子，不发光，受重力影响。
		/// </summary>
		public const int RedGrass1 = 117;
		/// <summary>
		/// 紫蓝白色粒子，不发光，受重力影响。
		/// </summary>
		public const int PurpleGems1 = 118;
		/// <summary>
		/// 深粉红色粒子，不发光，受重力影响。
		/// </summary>
		public const int PinkGems = 119;
		/// <summary>
		/// 深棕白色粒子，不发光，受重力影响。
		/// </summary>
		public const int PalePinkGems = 120;
		/// <summary>
		/// 深灰黑色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinGrey2 = 121;
		/// <summary>
		/// 深机械色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinIron = 122;
		/// <summary>
		/// 深粉红色粒子，不发光，受重力影响。
		/// </summary>
		public const int HotPinkBubble = 123;
		/// <summary>
		/// 浅黄偏白色粒子，不发光，受重力影响。
		/// </summary>
		public const int YellowWhiteBubble = 124;
		/// <summary>
		/// 深红偏黑色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinRed = 125;
		/// <summary>
		/// 深灰偏绿色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinGrey3 = 126;
		/// <summary>
		/// (!)岩浆色粒子，自发光，范围小，受重力影响。
		/// </summary>
		public const int OrangeFire = 127;
		/// <summary>
		/// 叶绿矿色粒子，不发光，受重力影响。
		/// </summary>
		public const int GreenGems = 128;
		/// <summary>
		/// 深土黄色粒子，不发光，受重力影响。
		/// </summary>
		public const int ThinBrown1 = 129;
		/// <summary>
		/// (!)白粉色粒子，自发光，范围较大，在重力下会呈现粒子下坠特效，在无重力时会悬停扩散，较快消散。
		/// </summary>
		public const int TrailingRed = 130;
		/// <summary>
		/// 发光曳尾 绿色 上升烟火, 发绿色光
		/// </summary>
		public const int TrailingGreen = 131;
		/// <summary>
		/// 发光曳尾 青色 掉落烟火, 发青色光
		/// </summary>
		public const int TrailingCyan = 132;
		/// <summary>
		/// 发光曳尾 黄色 掉落烟火, 发青色光
		/// </summary>
		public const int TrailingYellow = 133;
		/// <summary>
		/// 曳尾 粉色 掉落烟火
		/// </summary>
		public const int TrailingPink = 134;
		/// <summary>
		/// 发光青色 冰火把, 发青色光
		/// </summary>
		public const int IceTorch = 135;
		/// <summary>
		/// 红色材质
		/// </summary>
		public const int Red = 136;
		/// <summary>
		/// 亮蓝色/青色材质
		/// </summary>
		public const int BrightCyan = 137;
		/// <summary>
		/// 亮橙色/棕色材质
		/// </summary>
		public const int BrightOrange = 138;
		/// <summary>
		/// 青色 纸屑
		/// </summary>
		public const int CyanConfetti = 139;
		/// <summary>
		/// 绿色 纸屑
		/// </summary>
		public const int GreenConfetti = 140;
		/// <summary>
		/// 粉色 纸屑
		/// </summary>
		public const int PinkConfetti = 141;
		/// <summary>
		/// 黄色 纸屑
		/// </summary>
		public const int YellowConfetti = 142;
		/// <summary>
		///浅灰色 石块
		/// </summary>
		public const int LightGreyStone = 143;
		/// <summary>
		/// 艳铜 石块
		/// </summary>
		public const int CopperStone = 144;
		/// <summary>
		/// 粉色 石块
		/// </summary>
		public const int PinkStone = 145;
		/// <summary>
		/// 绿色/棕色材质 混合
		/// </summary>
		public const int GreenBrown = 146;
		/// <summary>
		/// 橙色材质
		/// </summary>
		public const int OrangeFx2 = 147;
		/// <summary>
		/// 饱和红色材质
		/// </summary>
		public const int RedDesaturated = 148;
		/// <summary>
		/// 白色材质
		/// </summary>
		public const int White = 149;
		/// <summary>
		/// 黑色/黄色/蓝白材质
		/// </summary>
		public const int BlackYellowBluishwhite = 150;
		/// <summary>
		/// 薄白色材质
		/// </summary>
		public const int ThinWhite = 151;
		/// <summary>
		/// 发光亮橙色 泡泡
		/// </summary>
		public const int OrangeBubble = 152;
		/// <summary>
		/// 亮橙色 泡泡材质
		/// </summary>
		public const int OrangeBubbleMaterial = 153;
		/// <summary>
		/// 薄苍白蓝色材质
		/// </summary>
		public const int BlueThin = 154;
		/// <summary>
		/// 薄暗棕色材质
		/// </summary>
		public const int DarkBrown = 155;
		/// <summary>
		/// 发光亮蓝色/白色 泡泡材质, 发苍白蓝色光
		/// </summary>
		public const int BlueWhiteBubble = 156;
		/// <summary>
		/// (.)薄绿色特效, 高亮
		/// </summary>
		public const int GreenFx = 157;
		/// <summary>
		/// 发光橙色 火焰, 发橙色光
		/// </summary>
		public const int OrangeFire1 = 158;
		/// <summary>
		/// 发光闪烁 黄色特效, 发黄色光
		/// </summary>
		public const int YellowFx = 159;
		/// <summary>
		/// 发光短暂 青色特效, 发亮青色光
		/// </summary>
		public const int CyanShortFx = 160;
		/// <summary>
		/// 青色材质
		/// </summary>
		public const int CyanMaterial = 161;
		/// <summary>
		/// 发光短暂 橙色特效, 发亮橙色光
		/// </summary>
		public const int OrangeShortFx = 162;
		/// <summary>
		/// (.)亮绿色 薄材质, 高亮
		/// </summary>
		public const int BrightGreen = 163;
		/// <summary>
		/// 发光flickering 粉色特效, 发桃红色光
		/// </summary>
		public const int PinkFx = 164;
		/// <summary>
		/// 白色/蓝色 泡泡材质
		/// </summary>
		public const int WhiteBlueBubble = 165;
		/// <summary>
		/// 薄亮粉色材质
		/// </summary>
		public const int PinkThinBright = 166;
		/// <summary>
		/// 薄绿色材质
		/// </summary>
		public const int ThinGreen = 167;
		/// <summary>
		/// !亮粉色 泡泡
		/// </summary>
		public const int PinkBrightBubble = 168;
		/// <summary>
		/// 发光黄色特效, 发深黄色光
		/// </summary>
		public const int YellowFx1 = 169;
		/// <summary>
		/// (.)薄橙色特效, 发微弱白色光
		/// </summary>
		public const int Ichor = 170;
		/// <summary>
		/// 亮紫色 泡泡材质
		/// </summary>
		public const int PurpleBubble = 171;
		/// <summary>
		/// (.)浅蓝色 微尘, 发微弱蓝色光
		/// </summary>
		public const int BlueParticle = 172;
		/// <summary>
		/// 发光短暂 紫色特效, 发亮紫色光
		/// </summary>
		public const int PurpleShortFx = 173;
		/// <summary>
		/// 发光亮橙色 泡泡材质, 发橙红色光
		/// </summary>
		public const int OrangeFire2 = 174;
		/// <summary>
		/// (.)短暂 白色特效, 高亮
		/// </summary>
		public const int WhiteShortFx = 175;
		/// <summary>
		///浅蓝色 微尘
		/// </summary>
		public const int LightBlueParticle = 176;
		/// <summary>
		///浅粉色 微尘
		/// </summary>
		public const int LightPinkParticle = 177;
		/// <summary>
		///浅绿色 微尘
		/// </summary>
		public const int LightGreenParticle = 178;
		/// <summary>
		///浅紫色 微尘
		/// </summary>
		public const int LightPurpleParticle = 179;
		/// <summary>
		/// 发光浅青色 微尘, 高亮
		/// </summary>
		public const int LightCyanParticle = 180;
		/// <summary>
		/// (.)浅青色/粉色 泡泡材质, 高亮
		/// </summary>
		public const int CyanPinkBubble = 181;
		/// <summary>
		/// (.)浅红色 泡泡材质, 几乎不发红色光
		/// </summary>
		public const int RedBubble = 182;
		/// <summary>
		/// (.)透明 红色 泡泡材质, 高亮
		/// </summary>
		public const int RedTransBubble = 183;
		/// <summary>
		/// 枯绿色、绿灰色 微尘 在地面停留
		/// </summary>
		public const int GreenishGreyParticle = 184;
		/// <summary>
		/// 发光浅青色 水晶材质, 发青色光
		/// </summary>
		public const int CyanCrystal = 185;
		/// <summary>
		/// 苍白暗蓝色 烟
		/// </summary>
		public const int DarkBlueSmoke = 186;
		/// <summary>
		/// 发光浅青色 微尘, 发青色光
		/// </summary>
		public const int LightCyanParticle1 = 187;
		/// <summary>
		/// 亮绿色 泡泡
		/// </summary>
		public const int GreenBubble1 = 188;
		/// <summary>
		/// 薄橙色材质
		/// </summary>
		public const int OrangeMaterial = 189;
		/// <summary>
		/// 薄金色材质
		/// </summary>
		public const int GoldMaterial = 190;
		/// <summary>
		/// 黑色 雪花
		/// </summary>
		public const int BlackFlakes = 191;
		/// <summary>
		/// 雪材质
		/// </summary>
		public const int SnowMaterial = 192;
		/// <summary>
		/// 绿色材质
		/// </summary>
		public const int GreenMaterial1 = 193;
		/// <summary>
		/// 薄棕色材质
		/// </summary>
		public const int BrownMaterial = 194;
		/// <summary>
		/// 薄黑色材质
		/// </summary>
		public const int BlackMaterial2 = 195;
		/// <summary>
		/// 薄绿色材质
		/// </summary>
		public const int ThinGreen1 = 196;
		/// <summary>
		/// (.)薄亮青色材质, 高亮
		/// </summary>
		public const int BrightCyanMaterial = 197;
		/// <summary>
		/// 黑色/白色 微尘
		/// </summary>
		public const int BlackWhiteParticle = 198;
		/// <summary>
		/// 苍白 紫色/黑色/灰色 微尘
		/// </summary>
		public const int PurpleBlackGrey = 199;
		/// <summary>
		/// 粉色 微尘
		/// </summary>
		public const int PinkParticle = 200;
		/// <summary>
		///浅粉色 微尘
		/// </summary>
		public const int LightPinkParticle1 = 201;
		/// <summary>
		///浅青色 微尘
		/// </summary>
		public const int LightCyanParticle2 = 202;
		/// <summary>
		/// 灰色 微尘
		/// </summary>
		public const int GreyParticle = 203;
		/// <summary>
		/// (.)白色 微尘, 高亮
		/// </summary>
		public const int WhiteParticle = 204;
		/// <summary>
		/// (.)薄粉色材质, 几乎不发粉色光
		/// </summary>
		public const int ThinPinkMaterial = 205;
		/// <summary>
		/// 发光短暂 青色特效, 发亮蓝色光
		/// </summary>
		public const int CyanShortFx1 = 206;
		/// <summary>
		/// 薄棕色材质
		/// </summary>
		public const int BrownMaterial1 = 207;
		/// <summary>
		/// 橙色 石块
		/// </summary>
		public const int OrangeStone = 208;
		/// <summary>
		/// 苍白 绿色 石块
		/// </summary>
		public const int PaleGreenStone = 209;
		/// <summary>
		/// off 白色材质
		/// </summary>
		public const int OffWhite = 210;
		/// <summary>
		/// 亮蓝色 微尘
		/// </summary>
		public const int BrightBlueParticle = 211;
		/// <summary>
		/// 白色 微尘
		/// </summary>
		public const int WhiteParticle1 = 212;
		/// <summary>
		/// (.)短暂 微白色特效, 几乎不发白色光
		/// </summary>
		public const int WhiteShortFx1 = 213;
		/// <summary>
		/// 薄苍白 棕色材质
		/// </summary>
		public const int Thin = 214;
		/// <summary>
		/// 薄khaki材质
		/// </summary>
		public const int ThinKhaki = 215;
		/// <summary>
		/// 苍白 粉色材质
		/// </summary>
		public const int Pale = 216;
		/// <summary>
		/// 青色 微尘
		/// </summary>
		public const int Cyan = 217;
		/// <summary>
		/// 桃红色 微尘
		/// </summary>
		public const int Hot = 218;
		/// <summary>
		/// 发光曳尾 红色 飞行着的烟火, 发橙色光
		/// </summary>
		public const int TrailingRed1 = 219;
		/// <summary>
		/// 发光曳尾 绿色 飞行着的烟火, 发绿色光
		/// </summary>
		public const int TrailingGreen1 = 220;
		/// <summary>
		/// 发光曳尾 蓝色 飞行着的烟火, 发苍白蓝色光
		/// </summary>
		public const int TrailingBlue = 221;
		/// <summary>
		/// 发光曳尾 黄色 飞行着的烟火, 发黄色光
		/// </summary>
		public const int TrailingYellow1 = 222;
		/// <summary>
		/// (.)曳尾 红色 飞行着的烟火, 高亮
		/// </summary>
		public const int TrailingRed2 = 223;
		/// <summary>
		/// 薄蓝色材质
		/// </summary>
		public const int ThinBlue = 224;
		/// <summary>
		/// 橙色材质
		/// </summary>
		public const int OrangeMaterial1 = 225;
		/// <summary>
		/// 
		/// </summary>
		public const int ElectricCyan = 226;

		/// <summary>
		/// 发光月炎 火焰!!!
		/// </summary>
		public const int CyanLunarFire = 229;
		/// <summary>
		/// 发光闪烁 紫色特效, 发紫光 
		/// </summary>
		public const int PurpleFx = 230;
	}
}
