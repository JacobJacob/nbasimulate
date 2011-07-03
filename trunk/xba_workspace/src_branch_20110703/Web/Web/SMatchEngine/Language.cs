namespace Web.SMatchEngine
{
    using System;
    using System.Text;

    public class Language
    {
        public static Random rnd = new Random(DateTime.Now.Millisecond);

        public static string BadOffenseBreak(string off, string pass, string def)
        {
            int num = rnd.Next(0, 7);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("胯下运球，晃过");
                    builder.Append(def);
                    builder.Append("的防守，中投不进。");
                    break;

                case 1:
                    builder.Append(def);
                    builder.Append("滑步紧逼，");
                    builder.Append(off);
                    builder.Append("跳投不中。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("突破中出手，被");
                    builder.Append(def);
                    builder.Append("的防守干扰球没进。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("甩开");
                    builder.Append(def);
                    builder.Append("接球上篮不进。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("防守下抛射失手。");
                    break;

                case 5:
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("的防守下，");
                    builder.Append(off);
                    builder.Append("运球转身勾手不进。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("突破上篮不中。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("突破受到");
                    builder.Append(def);
                    builder.Append("阻挡，跳投不中。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("受到夹击，起步打板不进。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("三步上篮，甩开");
                    builder.Append(def);
                    builder.Append("出手不中。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("突破后打板，球弹筐滑出。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("反手上篮不中。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("单打，跑投失手。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("突破，挑篮不中。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("的防守，空档处投篮偏出。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("转身，摆脱");
                    builder.Append(def);
                    builder.Append("，上篮失手。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("投篮偏出。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("突破后抛投偏出。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("突破后扣篮不中。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("突破后打板偏出。");
                    break;

                case 20:
                    builder.Append(def);
                    builder.Append("放低重心，用身体顶住，");
                    builder.Append(off);
                    builder.Append("的强行投篮偏出。");
                    break;

                case 0x15:
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("积极的防守，");
                    builder.Append(off);
                    builder.Append("疲于奔命，球投了个三不粘。");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("连续变向突破，");
                    builder.Append(def);
                    builder.Append("总能防住去路，");
                    builder.Append(off);
                    builder.Append("被迫在远离篮筐的位置出手,不中。");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("突破，中投的角度被");
                    builder.Append(def);
                    builder.Append("防死了，只好勾手，球砸在篮脖子上。");
                    break;

                case 0x18:
                    builder.Append(off);
                    builder.Append("跨步上篮，");
                    builder.Append(def);
                    builder.Append("贴身紧逼，");
                    builder.Append(off);
                    builder.Append("只得从篮板另一侧勉强勾手，不中。");
                    break;

                case 0x19:
                    builder.Append(off);
                    builder.Append("顶住");
                    builder.Append(def);
                    builder.Append("的贴身防守，后仰投篮，投出个三不粘！");
                    break;

                case 0x1a:
                    builder.Append(def);
                    builder.Append("防守时高高跳起，");
                    builder.Append(off);
                    builder.Append("被迫将突破中投的弧度抬高，球砸在篮筐前沿。");
                    break;

                case 0x1b:
                    builder.Append(off);
                    builder.Append("带球转身，甩开");
                    builder.Append(def);
                    builder.Append("顺势扣篮，不中。");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseBreakBlock(string off, string def)
        {
            int num = rnd.Next(0, 11);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(def);
                    builder.Append("补位及时， 将突破后");
                    builder.Append(off);
                    builder.Append("的投篮盖掉。");
                    break;

                case 1:
                    builder.Append(def);
                    builder.Append("上扑下堵，");
                    builder.Append(off);
                    builder.Append("连平时十拿九稳的中投也以被盖结束！");
                    break;

                case 2:
                    builder.Append(def);
                    builder.Append("防守时卡位准确，");
                    builder.Append(off);
                    builder.Append("的强打，换回一记火锅！");
                    break;

                case 3:
                    builder.Append("没能摆脱");
                    builder.Append(def);
                    builder.Append("的拼命防守，");
                    builder.Append(off);
                    builder.Append("急停跳投，被");
                    builder.Append(def);
                    builder.Append("飞身把球盖下！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("变向突破，总是甩不掉");
                    builder.Append(def);
                    builder.Append("，强行出手，被");
                    builder.Append(def);
                    builder.Append("干扰，球不进。");
                    break;

                case 5:
                    builder.Append(def);
                    builder.Append("侵犯性的防守令");
                    builder.Append(off);
                    builder.Append("勉强出手，又遭");
                    builder.Append(def);
                    builder.Append("无情的封盖！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("突破跳投，皮球在空中被");
                    builder.Append(def);
                    builder.Append("封盖！");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("突破后顺势挑篮，可惜被及时补防的");
                    builder.Append(def);
                    builder.Append("把皮球煽飞！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("带球转身投篮，被");
                    builder.Append(def);
                    builder.Append("从身后盖掉！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("三步上篮，被");
                    builder.Append(def);
                    builder.Append("迎面盖掉！");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("扣篮，");
                    builder.Append(def);
                    builder.Append("赶上来将球盖掉！");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseInside(string off, string pass, string def)
        {
            int num = rnd.Next(0, 0x18);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(def);
                    builder.Append("防守，");
                    builder.Append(off);
                    builder.Append("内线投篮不中。");
                    break;

                case 1:
                    builder.Append(def);
                    builder.Append("奋力封盖，");
                    builder.Append(off);
                    builder.Append("出手弧度被迫升高，球投失了。");
                    break;

                case 2:
                    builder.Append(def);
                    builder.Append("篮下贴住");
                    builder.Append(off);
                    builder.Append("，导致后者出手投篮不中。");
                    break;

                case 3:
                    builder.Append(def);
                    builder.Append("高举双手干扰了");
                    builder.Append(off);
                    builder.Append("投篮，球没投进。");
                    break;

                case 4:
                    builder.Append(def);
                    builder.Append("贴身紧逼，");
                    builder.Append(off);
                    builder.Append("勉强出手不中。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("在和");
                    builder.Append(def);
                    builder.Append("的对抗中扣篮不进。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("篮底硬挤，球还没出手就被");
                    builder.Append(def);
                    builder.Append("打了下来。");
                    break;

                case 7:
                    builder.Append(def);
                    builder.Append("篮下卡好位置，");
                    builder.Append(off);
                    builder.Append("后仰投篮不中。");
                    break;

                case 8:
                    builder.Append(def);
                    builder.Append("干扰了");
                    builder.Append(off);
                    builder.Append("的内线投篮。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("被");
                    builder.Append(def);
                    builder.Append("贴身防守干扰，出手投篮不中。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("被防到进攻时间将尽，无奈出手抛射不进。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("晃过");
                    builder.Append(def);
                    builder.Append("投篮，");
                    builder.Append(def);
                    builder.Append("从后边干扰，球投偏了。");
                    break;

                case 12:
                    builder.Append(def);
                    builder.Append("内线抗住");
                    builder.Append(off);
                    builder.Append("的勾手，球打在篮筐前沿。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("内线投篮不中。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("内线强打");
                    builder.Append(def);
                    builder.Append("，投篮不中。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("的防守跳投，球砸在篮筐的后沿上弹了出来。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("持球转身打板，偏了。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("空中转身打板不中。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("挤到内线，顶住");
                    builder.Append(def);
                    builder.Append("防守，小勾手涮筐而出。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("后撤步，拉开");
                    builder.Append(def);
                    builder.Append("的防守距离，跳投不中。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("干扰下，正面跳投偏出。");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("罚球线前出手投篮，");
                    builder.Append(def);
                    builder.Append("伸手干扰了一下，球没投中！");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("干扰下，篮下正面跳投偏出。");
                    break;

                case 0x17:
                    builder.Append(def);
                    builder.Append("贴身防守，");
                    builder.Append(off);
                    builder.Append("篮下勾手不中。");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseInsideBlock(string off, string def)
        {
            int num = rnd.Next(0, 0x10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("强打篮下，球被");
                    builder.Append(def);
                    builder.Append("一个大帽盖了出去！");
                    break;

                case 1:
                    builder.Append(def);
                    builder.Append("飞身盖掉了");
                    builder.Append(off);
                    builder.Append("的底线跳投！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("内线勾手投篮，");
                    builder.Append(def);
                    builder.Append("奋力盖帽。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("内线拿球就要扣，没想到被");
                    builder.Append(def);
                    builder.Append("伸手把球打掉。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("罚球线处投篮，刚一出手就被");
                    builder.Append(def);
                    builder.Append("盖掉。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("篮底硬挤，还没出手就被");
                    builder.Append(def);
                    builder.Append("断了下来。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("左侧出手打板，");
                    builder.Append(def);
                    builder.Append("赶将上来把球煽飞！");
                    break;

                case 7:
                    builder.Append(def);
                    builder.Append("一个大冒盖掉");
                    builder.Append(off);
                    builder.Append("的投篮。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("篮下转身投篮，被");
                    builder.Append(def);
                    builder.Append("从正面盖了下来。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("篮底勾手，被");
                    builder.Append(def);
                    builder.Append("双手捂了下来。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("内线上篮，");
                    builder.Append(def);
                    builder.Append("伸手盖了。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("转身勾手，");
                    builder.Append(def);
                    builder.Append("奋力盖帽，一个大火锅！");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("倚住");
                    builder.Append(def);
                    builder.Append("，勾手，球被");
                    builder.Append(def);
                    builder.Append("盖掉了！");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("底线投篮，被");
                    builder.Append(def);
                    builder.Append("盖掉！");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("侧身上篮，被");
                    builder.Append(def);
                    builder.Append("一巴掌冒了下来！");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("内线起跳扣篮，");
                    builder.Append(def);
                    builder.Append("空中把球结结实实地盖了下来！");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffensePick(string off, string pass, string def)
        {
            int num = rnd.Next(0, 11);
            StringBuilder builder = new StringBuilder();
            builder.Append("[挡拆]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("挡拆后，寻得空挡，接");
                    builder.Append(pass);
                    builder.Append("传球，打板投篮，偏出。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("给");
                    builder.Append(off);
                    builder.Append("挡人，");
                    builder.Append(off);
                    builder.Append("跳投不中。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("挡拆后接");
                    builder.Append(pass);
                    builder.Append("传球，抛射，不进。");
                    break;

                case 3:
                    builder.Append(def);
                    builder.Append("跟出去防");
                    builder.Append(pass);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("无人防守情况下中投失手。");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("和");
                    builder.Append(off);
                    builder.Append("在打了一个挡拆后，");
                    builder.Append(off);
                    builder.Append("接球跳投未中。");
                    break;

                case 5:
                    builder.Append(pass);
                    builder.Append("外线为");
                    builder.Append(off);
                    builder.Append("挡人，");
                    builder.Append(off);
                    builder.Append("勾手打板不进。");
                    break;

                case 6:
                    builder.Append(pass);
                    builder.Append("外线挡住");
                    builder.Append(def);
                    builder.Append("。");
                    builder.Append(off);
                    builder.Append("中投不中。");
                    break;

                case 7:
                    builder.Append(pass);
                    builder.Append("和");
                    builder.Append(off);
                    builder.Append("挡拆配合，");
                    builder.Append(off);
                    builder.Append("外线投失。");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("和");
                    builder.Append(off);
                    builder.Append("三分线弧顶挡拆，甩开");
                    builder.Append(def);
                    builder.Append("后，");
                    builder.Append(off);
                    builder.Append("投篮不中。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("持球，");
                    builder.Append(pass);
                    builder.Append("跑上前来挡人，");
                    builder.Append(off);
                    builder.Append("突到内线挑篮偏出。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("挡拆后冲向篮下，空中接");
                    builder.Append(pass);
                    builder.Append("球扣篮被篮筐拒之门外。");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffensePickBlock(string off, string def)
        {
            int num = rnd.Next(0, 9);
            StringBuilder builder = new StringBuilder();
            builder.Append("[挡拆]");
            switch (num)
            {
                case 0:
                    builder.Append("队友击地传球给挡拆后的");
                    builder.Append(off);
                    builder.Append("，后者上篮被盖。");
                    break;

                case 1:
                    builder.Append("队友与");
                    builder.Append(off);
                    builder.Append("做了个挡拆配合，");
                    builder.Append(off);
                    builder.Append("接球投篮，被协防的");
                    builder.Append(def);
                    builder.Append("盖掉！");
                    break;

                case 2:
                    builder.Append("队友击地传球给挡拆后的");
                    builder.Append(off);
                    builder.Append("，后者扣篮被");
                    builder.Append(def);
                    builder.Append("活活盖了出去！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("挡了一下");
                    builder.Append(def);
                    builder.Append("，自己跑开接队友传球，投篮，");
                    builder.Append(def);
                    builder.Append("及时跟进，把球盖飞！");
                    break;

                case 4:
                    builder.Append("队友传球给");
                    builder.Append(off);
                    builder.Append("，后者的上篮");
                    builder.Append(def);
                    builder.Append("赏了一记火锅！");
                    break;

                case 5:
                    builder.Append("队友圈顶拿球，");
                    builder.Append(off);
                    builder.Append("后撤步接到队友传球上篮被");
                    builder.Append(def);
                    builder.Append("封盖了下来！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("在弧顶附近做挡拆，队友从右侧突破后击地传球给");
                    builder.Append(off);
                    builder.Append("投篮，");
                    builder.Append(def);
                    builder.Append("眼尖手快将球盖了下来。");
                    break;

                case 7:
                    builder.Append("队友为");
                    builder.Append(off);
                    builder.Append("挡人，");
                    builder.Append(off);
                    builder.Append("急停跳投，");
                    builder.Append(def);
                    builder.Append("起跳封盖，手尖还是触到皮球。");
                    break;

                case 8:
                    builder.Append("队友拉到外线为");
                    builder.Append(off);
                    builder.Append("挡人，");
                    builder.Append(off);
                    builder.Append("趁机出手，");
                    builder.Append(def);
                    builder.Append("从内线扑出来，盖掉皮球！");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseShot(string off, string pass, string def)
        {
            int num = rnd.Next(0, 0x21);
            StringBuilder builder = new StringBuilder();
            builder.Append("[外线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("的防守，远投不中。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("中距离出手，偏出。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("外线勉强投篮不中。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("交叉步运球，将");
                    builder.Append(def);
                    builder.Append("的重心晃偏，跳射不进。");
                    break;

                case 4:
                    builder.Append(def);
                    builder.Append("用身体顶住，");
                    builder.Append(off);
                    builder.Append("的转身投篮投失了！");
                    break;

                case 5:
                    builder.Append(def);
                    builder.Append("脚步迅速阻挡");
                    builder.Append(off);
                    builder.Append("突破，");
                    builder.Append(off);
                    builder.Append("只得强行出手远投，球投软了！");
                    break;

                case 6:
                    builder.Append(def);
                    builder.Append("的防守很到位，");
                    builder.Append(off);
                    builder.Append("运球撞在");
                    builder.Append(def);
                    builder.Append("身上，失去平衡的投篮大失水准。");
                    break;

                case 7:
                    builder.Append(def);
                    builder.Append("防守不及，但");
                    builder.Append(off);
                    builder.Append("的转身投篮，投偏了！");
                    break;

                case 8:
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("的干扰下，");
                    builder.Append(off);
                    builder.Append("外线跳投偏出！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("做了一个投篮假动作，");
                    builder.Append(def);
                    builder.Append("不为所动，");
                    builder.Append(off);
                    builder.Append("无奈跳起投篮，不进！");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("底线跳投，球偏了。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("做完假动作跳起投篮，不中。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("中距离打板偏出。");
                    break;

                case 13:
                    builder.Append(def);
                    builder.Append("防守的重心很低，");
                    builder.Append(off);
                    builder.Append("无法运球，仓促远投，球砸在篮筐前沿弹了出来！");
                    break;

                case 14:
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("的贴身防守，");
                    builder.Append(off);
                    builder.Append("后仰投篮，球投偏了！");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("左突右晃，");
                    builder.Append(def);
                    builder.Append("总挡在眼前，急停跳投偏了！");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("远投，");
                    builder.Append(def);
                    builder.Append("干扰了一下，球砸在篮板上！");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("右翼转身后仰出手，偏出。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("圈顶出手，偏出。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("急停跳投，偏出。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("转身勾手，被");
                    builder.Append(def);
                    builder.Append("干扰了，球打在篮筐前沿没进！");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("被");
                    builder.Append(def);
                    builder.Append("防到圈顶，远程发炮偏离篮筐！");
                    break;

                case 0x16:
                    builder.Append(def);
                    builder.Append("积极防守，");
                    builder.Append(off);
                    builder.Append("跳投不中！");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("防守下投篮失手！");
                    break;

                case 0x18:
                    builder.Append(def);
                    builder.Append("伸长双臂影响了");
                    builder.Append(off);
                    builder.Append("投篮，");
                    builder.Append(off);
                    builder.Append("勉强出手远投不中。");
                    break;

                case 0x19:
                    builder.Append(def);
                    builder.Append("“牛皮糖”贴身防守，");
                    builder.Append(off);
                    builder.Append("无奈跑动中跳投，偏出！");
                    break;

                case 0x1a:
                    builder.Append(def);
                    builder.Append("的防守很成功，");
                    builder.Append(off);
                    builder.Append("运球撞在");
                    builder.Append(def);
                    builder.Append("身上，无奈出手投篮没中！也没造成犯规。");
                    break;

                case 0x1b:
                    builder.Append(def);
                    builder.Append("满场盯防，");
                    builder.Append(off);
                    builder.Append("跑投失手！");
                    break;

                case 0x1c:
                    builder.Append(off);
                    builder.Append("交叉步运球，晃过");
                    builder.Append(def);
                    builder.Append("，跳射不中。");
                    break;

                case 0x1d:
                    builder.Append(def);
                    builder.Append("死缠烂打，");
                    builder.Append(off);
                    builder.Append("最拿手的45度角投篮失手！");
                    break;

                case 30:
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("的贴身防守，");
                    builder.Append(off);
                    builder.Append("急停跳投偏出！");
                    break;

                case 0x1f:
                    builder.Append(off);
                    builder.Append("被");
                    builder.Append(def);
                    builder.Append("用身体抗住，仓促出手打在篮筐上不进！");
                    break;

                case 0x20:
                    builder.Append(off);
                    builder.Append("控球转身，没摆脱");
                    builder.Append(def);
                    builder.Append("的防守，45度角打板失手！");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseShotBlock(string off, string def)
        {
            int num = rnd.Next(0, 7);
            StringBuilder builder = new StringBuilder();
            builder.Append("[外线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("右侧勾手，");
                    builder.Append(def);
                    builder.Append("双手捂了下来。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("罚球线外勾手，被");
                    builder.Append(def);
                    builder.Append("盖了。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("外线强行出手，");
                    builder.Append(def);
                    builder.Append("高高跃起，将球封盖！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("跑动中跳投，");
                    builder.Append(def);
                    builder.Append("及时跟上，起跳，给了一个火锅！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("失去平衡出手投篮，仍然被");
                    builder.Append(def);
                    builder.Append("无情的盖了出去。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("左翼底线出手，");
                    builder.Append(def);
                    builder.Append("起跳，手尖摸到了皮球，封盖成功！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("拿球面对");
                    builder.Append(def);
                    builder.Append("起跳，投篮，");
                    builder.Append(def);
                    builder.Append("反映迅速，给了");
                    builder.Append(off);
                    builder.Append("一个火锅！");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseThree(string off, string pass, string def)
        {
            int num = rnd.Next(0, 15);
            StringBuilder builder = new StringBuilder();
            builder.Append("[三分]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("三分远投不中。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("外线右翼投失三分。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("拿球面对");
                    builder.Append(def);
                    builder.Append("，没有犹豫，直接出手，没中。");
                    break;

                case 3:
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("贴身防守下，");
                    builder.Append(off);
                    builder.Append("远投不中。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("圈顶发炮，这个三分跳投投偏了。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("零度角三分不中。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("突然外线跳投，");
                    builder.Append(def);
                    builder.Append("迅速起跳差点盖到皮球。球投偏了。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("在外线连续运球未能摆脱");
                    builder.Append(def);
                    builder.Append("防守，突然出手，三分球不进。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("圈顶三分发炮，");
                    builder.Append(def);
                    builder.Append("干扰起作用了，球砸在篮筐上弹了出来。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("后撤步拉到三分线外，摆脱");
                    builder.Append(def);
                    builder.Append("出手不中。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("零度角出手投三分，");
                    builder.Append(def);
                    builder.Append("奋力封盖。球未未中！");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("防守下三分远投不中。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("被");
                    builder.Append(def);
                    builder.Append("的防到外线，");
                    builder.Append(off);
                    builder.Append("无奈外线出手，球砸筐弹出。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("无视");
                    builder.Append(def);
                    builder.Append("的外线防守，拿球便投，三分不进。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("三分线外旱地拔葱跳投不中。");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseThreeBlock(string off, string def)
        {
            int num = rnd.Next(0, 15);
            StringBuilder builder = new StringBuilder();
            builder.Append("[三分]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("外线受到");
                    builder.Append(def);
                    builder.Append("严密防守，三分被盖！");
                    break;

                case 1:
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("严密的防守下，");
                    builder.Append(off);
                    builder.Append("只有仓皇远投，还是被盖了下来！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("外线投篮，");
                    builder.Append(def);
                    builder.Append("起跳封盖！");
                    break;

                case 3:
                    builder.Append(def);
                    builder.Append("压迫上来，");
                    builder.Append(off);
                    builder.Append("零度角出手，被直接盖了下来！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("三分线外出手，被");
                    builder.Append(def);
                    builder.Append("盖了下来。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("三分线外寻到空挡，出手，被补防的");
                    builder.Append(def);
                    builder.Append("飞身盖帽！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("三分线突然起跳投篮，被反映迅速");
                    builder.Append(def);
                    builder.Append("飞身起跳，盖帽！");
                    break;

                case 7:
                    builder.Append(def);
                    builder.Append("的防守把");
                    builder.Append(off);
                    builder.Append("一直压迫在三分线外，");
                    builder.Append(off);
                    builder.Append("突然起跳射三分，仍然被");
                    builder.Append(def);
                    builder.Append("的长臂盖掉！");
                    break;
            }
            return builder.ToString();
        }

        public static string DefFoul(string off, string def)
        {
            int num = rnd.Next(0, 15);
            StringBuilder builder = new StringBuilder();
            builder.Append("[犯规]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("突破造成了");
                    builder.Append(def);
                    builder.Append("的防守犯规。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("往篮下硬挤，造成");
                    builder.Append(def);
                    builder.Append("犯规。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("突破上篮，");
                    builder.Append(def);
                    builder.Append("背后掏球犯规。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("运球被");
                    builder.Append(def);
                    builder.Append("犯规。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("运球，");
                    builder.Append(def);
                    builder.Append("防守时将");
                    builder.Append(off);
                    builder.Append("绊倒。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("高举篮球作势欲攻，被");
                    builder.Append(def);
                    builder.Append("一巴掌打到脸上，");
                    builder.Append(def);
                    builder.Append("犯规。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("高速突破，");
                    builder.Append(def);
                    builder.Append("眼看阻拦不住，无奈将");
                    builder.Append(off);
                    builder.Append("抱住。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("变向运球，");
                    builder.Append(def);
                    builder.Append("打手犯规。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("篮下虚晃，失去防守位置的");
                    builder.Append(def);
                    builder.Append("无奈拉住其胳膊。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("拿球，传球时遭到");
                    builder.Append(def);
                    builder.Append("打手犯规。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("背后运球杀到底线，造成");
                    builder.Append(def);
                    builder.Append("阻挡犯规。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("虚晃变向运球，");
                    builder.Append(def);
                    builder.Append("打手犯规。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("强打内线，");
                    builder.Append(def);
                    builder.Append("推人犯规。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("篮下卡位，造成");
                    builder.Append(def);
                    builder.Append("拉人犯规。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("运球变向，突破，");
                    builder.Append(def);
                    builder.Append("阻挡犯规。");
                    break;
            }
            return builder.ToString();
        }

        public static string DefRebound(string defReb)
        {
            int num = rnd.Next(0, 20);
            StringBuilder builder = new StringBuilder();
            builder.Append("[篮板]");
            switch (num)
            {
                case 0:
                    builder.Append("篮板被");
                    builder.Append(defReb);
                    builder.Append("抢到！");
                    break;

                case 1:
                    builder.Append(defReb);
                    builder.Append("提早判断落点，冲上去抢到篮板。");
                    break;

                case 2:
                    builder.Append("防守成功，");
                    builder.Append(defReb);
                    builder.Append("跳起摘下防守篮板。");
                    break;

                case 3:
                    builder.Append("队友把对手卡在外围，");
                    builder.Append(defReb);
                    builder.Append("轻松抢下防守篮板。");
                    break;

                case 4:
                    builder.Append(defReb);
                    builder.Append("抢到一个防守篮板。");
                    break;

                case 5:
                    builder.Append("篮板球弹到外围，被");
                    builder.Append(defReb);
                    builder.Append("抢到。");
                    break;

                case 6:
                    builder.Append("篮板被");
                    builder.Append(defReb);
                    builder.Append("抢到。");
                    break;

                case 7:
                    builder.Append(defReb);
                    builder.Append("起跳将球拨出来，再次跳起把球抓到手里。");
                    break;

                case 8:
                    builder.Append(defReb);
                    builder.Append("提前卡位，抓下防守篮板。");
                    break;

                case 9:
                    builder.Append(defReb);
                    builder.Append("在内线站好位置，控制了防守篮板。");
                    break;

                case 10:
                    builder.Append(defReb);
                    builder.Append("积极防守，抓到防守篮板。");
                    break;

                case 11:
                    builder.Append(defReb);
                    builder.Append("伸手抓住掉下来的防守篮板。");
                    break;

                case 12:
                    builder.Append(defReb);
                    builder.Append("从对方球员头顶抢下防守篮板。");
                    break;

                case 13:
                    builder.Append(defReb);
                    builder.Append("绕前，抢到篮板。");
                    break;

                case 14:
                    builder.Append(defReb);
                    builder.Append("将对手投篮不中的球抢下。");
                    break;

                case 15:
                    builder.Append(defReb);
                    builder.Append("冲到篮下，高高跃起单手将篮板抓到。");
                    break;

                case 0x10:
                    builder.Append(defReb);
                    builder.Append("在内线占到位置，控制了这次的防守篮板。");
                    break;

                case 0x11:
                    builder.Append(defReb);
                    builder.Append("从对方手中抢到防守篮板。");
                    break;

                case 0x12:
                    builder.Append(defReb);
                    builder.Append("抓下篮板。");
                    break;

                case 0x13:
                    builder.Append(defReb);
                    builder.Append("冲向篮下，正好接住落下来的篮板球。");
                    break;
            }
            return builder.ToString();
        }

        public static string GetOffenseInsideDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("内线接到");
                    builder.Append(pass);
                    builder.Append("的传球，跳起将球扣入。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("助攻");
                    builder.Append(off);
                    builder.Append("扣篮得分。");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("绕过");
                    builder.Append(def);
                    builder.Append("的防守，助攻");
                    builder.Append(off);
                    builder.Append("一个漂亮的上篮得分。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("倚住");
                    builder.Append(def);
                    builder.Append("，接到");
                    builder.Append(pass);
                    builder.Append("的传球，在");
                    builder.Append(def);
                    builder.Append("的头顶打板得分。");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("趁");
                    builder.Append(def);
                    builder.Append("内线失去防守位置，助攻");
                    builder.Append(off);
                    builder.Append("挑篮得分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("底线接到");
                    builder.Append(pass);
                    builder.Append("的传球，翻身大勾手得分。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("的助攻，篮下强打得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("篮下跑出空挡接");
                    builder.Append(pass);
                    builder.Append("的传球，轻松得分。");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("把球吊进内线，");
                    builder.Append(off);
                    builder.Append("靠身体顶住");
                    builder.Append(def);
                    builder.Append("，灌篮得分。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("防守，起跳空中接");
                    builder.Append(pass);
                    builder.Append("恰倒好处的传球，单手扣进蓝框！");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseBreak(string off, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("接球，突破");
                    builder.Append(def);
                    builder.Append("后打板进球。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("底线突破，上篮得分。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("快速突破转身，空中出手命中。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("连续的运球晃动，突然跳投得手。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("转身，摆脱");
                    builder.Append(def);
                    builder.Append("，上篮得分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("晃过");
                    builder.Append(def);
                    builder.Append("的防守，低手上篮得分。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("转身过了");
                    builder.Append(def);
                    builder.Append("，打板得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("接过传球，顺势跑投得手。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("底线转身，上篮得分。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("突破起跳，空中躲过");
                    builder.Append(def);
                    builder.Append("封盖，打板得分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseBreakDunk(string off, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("后扣篮成功！");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("的防守，背对篮筐反手扣篮！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("突破，双手灌篮得分！。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("胯下运球，顺势一个转身，左手将球灌进篮筐！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("加速，突破了");
                    builder.Append(def);
                    builder.Append("的防守，挑篮得手！");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("突到篮下打板得分。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("突破防守，挑篮得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("变向三步上篮得手。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("起步，顶住");
                    builder.Append(def);
                    builder.Append("贴身防守侧勾得手。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("持球在三秒区外游走，突然一个变向三步上篮得手！");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseBreakDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球后，突破，扣篮。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("传球给底线的");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("底线突破，双手扣篮。");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("突破分球，");
                    builder.Append(off);
                    builder.Append("篮下接球，轻松扣篮。");
                    break;

                case 3:
                    builder.Append(pass);
                    builder.Append("突破传球");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("拿球重扣。");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("转身，摆脱");
                    builder.Append(def);
                    builder.Append("，传球给");
                    builder.Append(off);
                    builder.Append("，后者上篮得分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，骗过");
                    builder.Append(def);
                    builder.Append("的防守，上篮得分。");
                    break;

                case 6:
                    builder.Append(pass);
                    builder.Append("假动作突破，把球传出，");
                    builder.Append(off);
                    builder.Append("转身过了");
                    builder.Append(def);
                    builder.Append("，轻松把球放进篮框。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("接过");
                    builder.Append(pass);
                    builder.Append("的传球，顺势扣篮。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("空切内线接");
                    builder.Append(pass);
                    builder.Append("传球，轻松扣篮。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("的击地传球，突破扣篮。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseBreakGoodDunk(string off, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("胯下运球转身将");
                    builder.Append(def);
                    builder.Append("甩到身后，面对篮筐扣篮得分！");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("拿球背对");
                    builder.Append(def);
                    builder.Append("，突然一个前转身溜进了底线双手大力扣篮！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("转身突破，勾手中的。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("顶住");
                    builder.Append(def);
                    builder.Append("的贴身防守，强行突破投篮，球进。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("快速的胯下运球，晃晕了");
                    builder.Append(def);
                    builder.Append("，突破打板得分！");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("做了个左侧突破的假动作，骗开");
                    builder.Append(def);
                    builder.Append("防守，高速从右侧单手扣篮！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("胯下运球，突然启动将");
                    builder.Append(def);
                    builder.Append("甩到身后轻松扣篮！");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("从罚球线冲向篮底，正面重扣！");
                    break;

                case 8:
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("的防守后，");
                    builder.Append(off);
                    builder.Append("大力扣篮得分！");
                    break;

                case 9:
                    builder.Append("突然变向，");
                    builder.Append(off);
                    builder.Append("摆脱防守，单手暴扣！");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseBreakGoodDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，快速突破底线，随后一记重扣！");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("传球到内线，");
                    builder.Append(off);
                    builder.Append("空切拿球，隔着防守球员就是一记扣篮！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("绕过防守，空切内线，接");
                    builder.Append(pass);
                    builder.Append("传球，扣篮得分！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，转身将");
                    builder.Append(def);
                    builder.Append("甩到身后，面对篮筐扣篮得分！");
                    builder.Append("传球，转身将");
                    builder.Append(def);
                    builder.Append("甩到身后，面对篮筐扣篮得分！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，起跳，空中躲过");
                    builder.Append(def);
                    builder.Append("封盖，拉杆扣篮！");
                    break;

                case 5:
                    builder.Append(pass);
                    builder.Append("助攻，");
                    builder.Append(off);
                    builder.Append("突到内线，转身后仰投篮命中！");
                    break;

                case 6:
                    builder.Append(pass);
                    builder.Append("助攻，");
                    builder.Append(off);
                    builder.Append("加快脚步，冲到禁区左勾手打板得分！");
                    break;

                case 7:
                    builder.Append(pass);
                    builder.Append("助攻，");
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("的防守，面对篮筐单手扣篮！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("甩开");
                    builder.Append(def);
                    builder.Append("，突然一个前转身溜进了底线接接");
                    builder.Append(pass);
                    builder.Append("恰到好处的传球，扣篮！");
                    break;

                case 9:
                    builder.Append(pass);
                    builder.Append("助攻，");
                    builder.Append(off);
                    builder.Append("一个加速，突破了");
                    builder.Append(def);
                    builder.Append("的防守，挑篮得手！");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseBreakWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球后，突破");
                    builder.Append(def);
                    builder.Append("后打板进球。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("传球给底线的");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("底线突破，上篮得分。");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("突破分球，");
                    builder.Append(off);
                    builder.Append("单打");
                    builder.Append(def);
                    builder.Append("，勾手投中。");
                    break;

                case 3:
                    builder.Append(pass);
                    builder.Append("突破后，传球给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("抛投得手。");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("转身，摆脱");
                    builder.Append(def);
                    builder.Append("，传球给无人防守的");
                    builder.Append(off);
                    builder.Append("上篮得分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，晃过");
                    builder.Append(def);
                    builder.Append("的防守，低手上篮得分。");
                    break;

                case 6:
                    builder.Append(pass);
                    builder.Append("突破未果，把球传出，");
                    builder.Append(off);
                    builder.Append("转身过了");
                    builder.Append(def);
                    builder.Append("，打板得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("接过");
                    builder.Append(pass);
                    builder.Append("的传球，顺势跑投得手。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("空切内线接");
                    builder.Append(pass);
                    builder.Append("传球，后仰投篮命中。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("的击地传球，突破起跳，空中躲过");
                    builder.Append(def);
                    builder.Append("封盖，打板得分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseInside(string off, string def)
        {
            int num = rnd.Next(0, 0x19);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("直插篮下打板得分。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("的面前跳投得分。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("篮下跳投得分。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("绕过");
                    builder.Append(def);
                    builder.Append("的防守，禁区投篮得分。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("倚住");
                    builder.Append(def);
                    builder.Append("，打板得分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("趁");
                    builder.Append(def);
                    builder.Append("内线失去防守位置，轻松得分。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("底线翻身擦板得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("篮下勾手得分。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("罚球线处接球，运球到篮下，轻松命中。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("倚住");
                    builder.Append(def);
                    builder.Append("，打板得分。");
                    break;

                case 10:
                    builder.Append(def);
                    builder.Append("内线失去防守位置，被");
                    builder.Append(off);
                    builder.Append("轻松得分。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("篮下勾手得分。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("转身投篮得分。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("从底线进攻，上篮得分。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("运球变向，篮下高手上篮得分。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("严防下奋力跳投，球进。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("用身体顶住");
                    builder.Append(def);
                    builder.Append("，正面勾手投中。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("内线投中一球。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("内线单打");
                    builder.Append(def);
                    builder.Append("得分。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("持球晃过");
                    builder.Append(def);
                    builder.Append("，轻松卡板得分。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("内线单打");
                    builder.Append(def);
                    builder.Append("得分。");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("底线出手得分。");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("运球到篮下，在");
                    builder.Append(def);
                    builder.Append("头顶投篮命中。");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("内线强吃");
                    builder.Append(def);
                    builder.Append("，遇到强硬防守，改半勾手命中。");
                    break;

                case 0x18:
                    builder.Append(off);
                    builder.Append("内线强打，零度角跳投得分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseInsideDunk(string off, string def)
        {
            int num = rnd.Next(0, 13);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("内线扣篮得分。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("单手扣篮。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("扣篮得分。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("侧对");
                    builder.Append(def);
                    builder.Append("，突然一个右转身，左勾手打板进筐。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("防守，把篮球扣进篮筐。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("旱地拔葱怒扣入筐！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("压着");
                    builder.Append(def);
                    builder.Append("的手将球重重扣入！");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("的头顶爆扣得分！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("在两名防守队员的夹击下毫不犹豫的高高跳起，翻身换手扣篮！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("在空中将球拉下来躲过");
                    builder.Append(def);
                    builder.Append("的封盖后又一挺身将球扣入！");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("在与");
                    builder.Append(def);
                    builder.Append("空中身体对抗后，仍把球狠狠扣入！");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("一记重扣！");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("绕过");
                    builder.Append(def);
                    builder.Append("双手狠狠将球塞入篮筐！");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseInsideGoodDunk(string off, string def)
        {
            int num = rnd.Next(0, 12);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("再现奥拉朱旺梦幻舞步，骗晕");
                    builder.Append(def);
                    builder.Append("，轻松挑篮得手。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("头顶一记重扣得分。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("防守，旱地拔葱灌篮。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("虚晃一下，直接杀进内线，在");
                    builder.Append(def);
                    builder.Append("头顶挑篮得手。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("顶到内线，连续假动作晃开");
                    builder.Append(def);
                    builder.Append("，轻松打板得分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("跳到空中，在");
                    builder.Append(def);
                    builder.Append("拦截下，拉杆扣篮得分！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("长驱直入，双手灌篮得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("取得绝对内线优势，单手扣篮，");
                    builder.Append(def);
                    builder.Append("只能仰视。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("挤开");
                    builder.Append(def);
                    builder.Append("的防守，来了一个霸王硬扣篮。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("闪开");
                    builder.Append(def);
                    builder.Append("防守，单手持球暴扣！");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("强打内线得手。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("空中躲过");
                    builder.Append(def);
                    builder.Append("封盖，拉杆扣篮。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseInsideGoodDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("内线接到");
                    builder.Append(pass);
                    builder.Append("的传球，毫不犹豫翻身一记重扣！");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("助攻");
                    builder.Append(off);
                    builder.Append("双手狠狠将球塞入篮筐！");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("绕过");
                    builder.Append(def);
                    builder.Append("的防守，将球传给");
                    builder.Append(off);
                    builder.Append("爆扣得分！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("倚住");
                    builder.Append(def);
                    builder.Append("，接到");
                    builder.Append(pass);
                    builder.Append("的传球后，翻身换手扣篮！");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("见");
                    builder.Append(def);
                    builder.Append("内线失去防守位置，将球抛向内线，");
                    builder.Append(off);
                    builder.Append("空中接球灌篮！");
                    break;

                case 5:
                    builder.Append(pass);
                    builder.Append("将球扔向篮板，");
                    builder.Append(off);
                    builder.Append("高高跃起，隔着");
                    builder.Append(def);
                    builder.Append("就是一记灌篮！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("在空中接到");
                    builder.Append(pass);
                    builder.Append("的传球，避开");
                    builder.Append(def);
                    builder.Append("的拦截，换手扣篮得分！");
                    break;

                case 7:
                    builder.Append(def);
                    builder.Append("稍一松懈，被");
                    builder.Append(pass);
                    builder.Append("绕过将球传给内线");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("重扣得分！");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("将球抛向篮筐，");
                    builder.Append(off);
                    builder.Append("空中抓球一个大回环，漂亮的风车灌篮！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("内线接");
                    builder.Append(pass);
                    builder.Append("的传球，面队防守，高高跃起，双手扣篮。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseInsideWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("内线接到");
                    builder.Append(pass);
                    builder.Append("的传球，轻松投中一球。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("助攻");
                    builder.Append(off);
                    builder.Append("篮下跳投得分。");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("绕过");
                    builder.Append(def);
                    builder.Append("的防守，助攻");
                    builder.Append(off);
                    builder.Append("禁区投篮得分。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("倚住");
                    builder.Append(def);
                    builder.Append("，接到");
                    builder.Append(pass);
                    builder.Append("的传球，打板得分。");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("趁");
                    builder.Append(def);
                    builder.Append("内线失去防守位置，助攻");
                    builder.Append(off);
                    builder.Append("轻松得分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("底线接到");
                    builder.Append(pass);
                    builder.Append("的传球，翻身得分。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("的助攻，篮下勾手得分。");
                    break;

                case 7:
                    builder.Append(pass);
                    builder.Append("做了个假动作，骗开");
                    builder.Append(def);
                    builder.Append("，助攻篮下的");
                    builder.Append(off);
                    builder.Append("得分。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("篮下接");
                    builder.Append(pass);
                    builder.Append("恰倒好处的传球，跳投得分。");
                    break;

                case 9:
                    builder.Append(pass);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("横传篮下，助攻");
                    builder.Append(off);
                    builder.Append("得分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffensePick(string off, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[挡拆]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("挡拆配合后，打板得分。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("挡拆，上篮得分。");
                    break;

                case 2:
                    builder.Append("打了一个漂亮的挡拆后，");
                    builder.Append(off);
                    builder.Append("轻松的跳投命中。");
                    break;

                case 3:
                    builder.Append("在队友挡人的帮助下，");
                    builder.Append(off);
                    builder.Append("迅速突破篮下，上篮得分。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("挡拆后，空切篮下，得球上篮得分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("和队友巧妙挡拆后，跑空篮得手。");
                    break;

                case 6:
                    builder.Append("教科书式的挡拆配合后，");
                    builder.Append(off);
                    builder.Append("上篮得分。");
                    break;

                case 7:
                    builder.Append("两个队友帮助");
                    builder.Append(off);
                    builder.Append("挡人，");
                    builder.Append(off);
                    builder.Append("攻入篮下，轻松挑篮得分。");
                    break;

                case 8:
                    builder.Append("三分线弧顶挡拆后，甩开");
                    builder.Append(def);
                    builder.Append("后，");
                    builder.Append(off);
                    builder.Append("接妙传上篮得分。");
                    break;

                case 9:
                    builder.Append("挡拆配合扰乱对方防守，");
                    builder.Append(off);
                    builder.Append("快速突入篮下，投中两分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffensePickDunk(string off, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[挡拆]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("挡拆配合后，打板得分。");
                    break;

                case 1:
                    builder.Append("队友拉到外线为");
                    builder.Append(off);
                    builder.Append("挡人，");
                    builder.Append(off);
                    builder.Append("切入底线，大力灌篮！精彩！");
                    break;

                case 2:
                    builder.Append("打了一个漂亮的挡拆后，");
                    builder.Append(off);
                    builder.Append("轻松的跳投命中。");
                    break;

                case 3:
                    builder.Append("在队友挡人的帮助下，");
                    builder.Append(off);
                    builder.Append("迅速突破篮下，上篮得分。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("挡拆后，空切篮下，得球上篮得分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("和队友巧妙挡拆后，跑空篮得手。");
                    break;

                case 6:
                    builder.Append("教科书式的挡拆配合后，");
                    builder.Append(off);
                    builder.Append("上篮得分。");
                    break;

                case 7:
                    builder.Append("两个队友帮助");
                    builder.Append(off);
                    builder.Append("挡人，");
                    builder.Append(off);
                    builder.Append("攻入篮下，轻松挑篮得分。");
                    break;

                case 8:
                    builder.Append("三分线弧顶挡拆后，甩开");
                    builder.Append(def);
                    builder.Append("后，");
                    builder.Append(off);
                    builder.Append("接妙传上篮得分。");
                    break;

                case 9:
                    builder.Append("挡拆配合扰乱对方防守，");
                    builder.Append(off);
                    builder.Append("快速突入篮下，投中两分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffensePickDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[挡拆]");
            switch (num)
            {
                case 0:
                    builder.Append("无球挡拆后，");
                    builder.Append(off);
                    builder.Append("篮下空挡接");
                    builder.Append(pass);
                    builder.Append("传球，轻松扣篮得手！");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("与");
                    builder.Append(off);
                    builder.Append("做了个挡拆配合，");
                    builder.Append(off);
                    builder.Append("接球就是一记扣篮！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("挡住");
                    builder.Append(def);
                    builder.Append("，跑出位置接");
                    builder.Append(pass);
                    builder.Append("传球挑篮得分。");
                    break;

                case 3:
                    builder.Append(pass);
                    builder.Append("击地传球给挡拆后的");
                    builder.Append(off);
                    builder.Append("，后者上篮轻松取分。");
                    break;

                case 4:
                    builder.Append(def);
                    builder.Append("跟出去防");
                    builder.Append(pass);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("在无人看防的情况下接");
                    builder.Append(pass);
                    builder.Append("传球命中。");
                    break;

                case 5:
                    builder.Append(pass);
                    builder.Append("圈顶拿球，");
                    builder.Append(off);
                    builder.Append("做墙撤步转身的同时，接到");
                    builder.Append(pass);
                    builder.Append("传球，漂亮扣篮！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("在弧顶附近做挡拆，");
                    builder.Append(pass);
                    builder.Append("右侧突破后，传球给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("接球后中投得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("挡拆后冲向篮下，空中接");
                    builder.Append(pass);
                    builder.Append("传球，接力得分。");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("与");
                    builder.Append(off);
                    builder.Append("做了个挡拆配合，");
                    builder.Append(off);
                    builder.Append("接球投篮命中。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("与");
                    builder.Append(pass);
                    builder.Append("挡拆后空切，人到球到，");
                    builder.Append(off);
                    builder.Append("篮下轻松扣篮。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffensePickGoodDunk(string off, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[挡拆]");
            switch (num)
            {
                case 0:
                    builder.Append("队友和");
                    builder.Append(off);
                    builder.Append("挡拆配合后，");
                    builder.Append(off);
                    builder.Append("切入内线，在");
                    builder.Append(def);
                    builder.Append("的头顶暴扣！");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("和队友做了个挡拆，强攻内线，以一记扣篮羞辱了防守已经失位的");
                    builder.Append(def);
                    builder.Append("！");
                    break;

                case 2:
                    builder.Append("打了一个漂亮的挡拆后，");
                    builder.Append(def);
                    builder.Append("被甩在后面，");
                    builder.Append(off);
                    builder.Append("轻松扣中一球。");
                    break;

                case 3:
                    builder.Append("在队友挡人的帮助下，");
                    builder.Append(off);
                    builder.Append("迅速突破篮下，双手灌框。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("挡拆后，空切篮下，得球空中接力扣篮，漂亮的两分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("和队友巧妙挡拆后，跑空篮，玩了个大风车扣篮。");
                    break;

                case 6:
                    builder.Append("教科书式的挡拆配合后，");
                    builder.Append(off);
                    builder.Append("突破，玩了个换手反扣！SHOW！");
                    break;

                case 7:
                    builder.Append("两个队友帮助");
                    builder.Append(off);
                    builder.Append("挡人，");
                    builder.Append(off);
                    builder.Append("攻入篮下，单手扣篮。");
                    break;

                case 8:
                    builder.Append("三分线弧顶挡拆后，甩开");
                    builder.Append(def);
                    builder.Append("后，");
                    builder.Append(off);
                    builder.Append("接妙传跳篮得分。");
                    break;

                case 9:
                    builder.Append("挡拆配合扰乱对方防守，");
                    builder.Append(off);
                    builder.Append("快速突入篮下，暴扣！");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffensePickGoodDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[挡拆]");
            switch (num)
            {
                case 0:
                    builder.Append("无球挡拆后，");
                    builder.Append(off);
                    builder.Append("篮下空挡高高跃起，接");
                    builder.Append(pass);
                    builder.Append("传球，单手大力扣篮！");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("与");
                    builder.Append(off);
                    builder.Append("挡拆配合，甩开防守，");
                    builder.Append(off);
                    builder.Append("接球后，就是一记重扣！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("与");
                    builder.Append(def);
                    builder.Append("挡拆，没有跑出空挡，");
                    builder.Append(off);
                    builder.Append("拿球突破篮下，隔着");
                    builder.Append(def);
                    builder.Append("就是一记扣篮！");
                    break;

                case 3:
                    builder.Append(pass);
                    builder.Append("击地传球给");
                    builder.Append(off);
                    builder.Append("，当");
                    builder.Append(def);
                    builder.Append("防守到位，");
                    builder.Append(off);
                    builder.Append("已经腾在空中了。这次进攻以");
                    builder.Append(off);
                    builder.Append("的重扣结束。");
                    break;

                case 4:
                    builder.Append(def);
                    builder.Append("跟出去防");
                    builder.Append(pass);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("在内线接");
                    builder.Append(pass);
                    builder.Append("传球扣中一球。");
                    break;

                case 5:
                    builder.Append(pass);
                    builder.Append("圈顶拿球，");
                    builder.Append(off);
                    builder.Append("欲做挡拆，突然空切篮下，空中接");
                    builder.Append(pass);
                    builder.Append("传球，扣篮！精彩！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("在弧顶附近做挡拆，");
                    builder.Append(pass);
                    builder.Append("右侧突破后，传球给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("接球后突破篮下扣篮。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("挡拆后冲向篮下，空中接");
                    builder.Append(pass);
                    builder.Append("传球，重扣！经典瞬间！");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("与");
                    builder.Append(off);
                    builder.Append("做了个挡拆配合，没有撤出空挡，");
                    builder.Append(off);
                    builder.Append("凭借超强的个人能力，突破得分！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("与");
                    builder.Append(pass);
                    builder.Append("挡拆后空切，人到球到，");
                    builder.Append(off);
                    builder.Append("篮下双手轻松扣篮。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffensePickWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 11);
            StringBuilder builder = new StringBuilder();
            builder.Append("[挡拆]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("挡拆配合后，接");
                    builder.Append(pass);
                    builder.Append("传球打板得分。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("给");
                    builder.Append(off);
                    builder.Append("挡人，");
                    builder.Append(off);
                    builder.Append("跳投得分。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("挡拆后接");
                    builder.Append(pass);
                    builder.Append("传球上篮得分。");
                    break;

                case 3:
                    builder.Append(def);
                    builder.Append("跟出去防");
                    builder.Append(pass);
                    builder.Append("，结果");
                    builder.Append(off);
                    builder.Append("无人防守，接");
                    builder.Append(pass);
                    builder.Append("传球中投命中。");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("和");
                    builder.Append(off);
                    builder.Append("在打了一个漂亮的挡拆后，");
                    builder.Append(off);
                    builder.Append("轻松的跳投命中。");
                    break;

                case 5:
                    builder.Append(pass);
                    builder.Append("和");
                    builder.Append(off);
                    builder.Append("巧妙挡拆，后者接传球，跑空篮得手。");
                    break;

                case 6:
                    builder.Append("教科书式的挡拆配合后，");
                    builder.Append(pass);
                    builder.Append("击地传球给的");
                    builder.Append(off);
                    builder.Append("，后者上篮得分。");
                    break;

                case 7:
                    builder.Append(pass);
                    builder.Append("和");
                    builder.Append(off);
                    builder.Append("挡拆配合，帮助");
                    builder.Append(off);
                    builder.Append("外线投中2分。");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("和");
                    builder.Append(off);
                    builder.Append("三分线弧顶挡拆，甩开");
                    builder.Append(def);
                    builder.Append("后，");
                    builder.Append(off);
                    builder.Append("接妙传上篮得分。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("进攻，");
                    builder.Append(pass);
                    builder.Append("跑上前来挡人，");
                    builder.Append(off);
                    builder.Append("突到挑篮得分。");
                    break;

                case 10:
                    builder.Append("挡拆配合扰乱对方防守，");
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("妙传，轻松出手，中投两分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseShot(string off, string def)
        {
            int num = rnd.Next(0, 0x23);
            StringBuilder builder = new StringBuilder();
            builder.Append("[外线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("跑动中摆脱");
                    builder.Append(def);
                    builder.Append("后仰跳投命中！");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("持球虚晃");
                    builder.Append(def);
                    builder.Append("，左侧45度角打板得分！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("往左虚晃");
                    builder.Append(def);
                    builder.Append("，闪到右侧中投得手！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("背对" + def + "，转身后仰投篮，球进。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("零度角面对");
                    builder.Append(def);
                    builder.Append("的封盖，后仰投篮命中！");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("体前来回运球，突然后仰投篮命中！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("弧顶虚晃");
                    builder.Append(def);
                    builder.Append("，远投得手！");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("持球做传球状，晃开");
                    builder.Append(def);
                    builder.Append("，跳投得手！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("胯下运球，一个后撤步甩开");
                    builder.Append(def);
                    builder.Append("，远投得手！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("禁区外背打");
                    builder.Append(def);
                    builder.Append("，勾手居然将球蒙中！");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("中投得手。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("转身，换手跳投得分。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("外线运球游走，突射冷箭球进！");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("罚球线外后仰中投命中！");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("突破，急停，一个旱地拔葱跳投得分！");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("的防守，直接跳投得分。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("中距离出手，投中一球。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("罚球线附近避开");
                    builder.Append(def);
                    builder.Append("干扰，跳投得分。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("强行突破，跳投得手。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("胯下运球，外线跳投命中。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("禁区右侧压住");
                    builder.Append(def);
                    builder.Append("勾手把球投中。");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("度角晃过");
                    builder.Append(def);
                    builder.Append("跳投得分。");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("右翼跳投得分。");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("投篮得分。");
                    break;

                case 0x18:
                    builder.Append(off);
                    builder.Append("后仰投篮进了。");
                    break;

                case 0x19:
                    builder.Append(off);
                    builder.Append("篮板左侧打板得分。");
                    break;

                case 0x1a:
                    builder.Append(off);
                    builder.Append("在底线出手，把球投中。");
                    break;

                case 0x1b:
                    builder.Append(off);
                    builder.Append("罚球线附近转身投篮命中。");
                    break;

                case 0x1c:
                    builder.Append(off);
                    builder.Append("跑动中后仰跳投命中！");
                    break;

                case 0x1d:
                    builder.Append(off);
                    builder.Append("突破，果断得急停跳投得分！");
                    break;

                case 30:
                    builder.Append(off);
                    builder.Append("拿球单打，转身45度角打板得分！");
                    break;

                case 0x1f:
                    builder.Append(off);
                    builder.Append("后撤步，远投命中！");
                    break;

                case 0x20:
                    builder.Append(off);
                    builder.Append("趁对方防守松懈的间隙跳投得分。");
                    break;

                case 0x21:
                    builder.Append(off);
                    builder.Append("快速突破，远抛得手！");
                    break;

                case 0x22:
                    builder.Append(off);
                    builder.Append("篮板右翼中投命中！");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseShotWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 9);
            StringBuilder builder = new StringBuilder();
            builder.Append("[外线]");
            switch (num)
            {
                case 0:
                    builder.Append(pass);
                    builder.Append("佯装突破，分球给外线做好准备的");
                    builder.Append(off);
                    builder.Append("，后者远投空心入网。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("突破分球给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("远投打板入筐。");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("分球到外线的");
                    builder.Append(off);
                    builder.Append("，后者果断出手，命中！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，果断出手，空心入网。");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("突破后把球分给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("闪开");
                    builder.Append(def);
                    builder.Append("封盖，跳投得分。");
                    break;

                case 5:
                    builder.Append(pass);
                    builder.Append("底线位置把球传给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("圈顶出手命中。");
                    break;

                case 6:
                    builder.Append(pass);
                    builder.Append("突破后助攻底线投手");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("稳稳命中两分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，后撤步跳投中的。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("传");
                    builder.Append(pass);
                    builder.Append("后,");
                    builder.Append("在外线找到空挡，接");
                    builder.Append(pass);
                    builder.Append("及时的传球，命中两分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseThree(string off, string def)
        {
            int num = rnd.Next(0, 0x11);
            StringBuilder builder = new StringBuilder();
            builder.Append("&lt;font color sb.Append( blue&gt;[三分]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("三分球出手命中。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("三分远投中的。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("防守下三分远投中的。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("外线左翼飙中三分。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("外线右翼飙中三分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("外线持球，在");
                    builder.Append(def);
                    builder.Append("防守下突然投篮，三分中的。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("弧顶投中三分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("超远距离三分命中。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("零度角出手，");
                    builder.Append(def);
                    builder.Append("防守不及，球进。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("距离三分线还有一步的地方出手，命中。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("佯装突破，突然外线出手，三分中的。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("外线晃过");
                    builder.Append(def);
                    builder.Append("出手命中三分。");
                    break;

                case 12:
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("的贴身防守下，");
                    builder.Append(off);
                    builder.Append("无奈出手，球居然打板入网。");
                    break;

                case 13:
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("防守，");
                    builder.Append(off);
                    builder.Append("三分线外拿球便投，球进了。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("拿球后撤步，退到三分线外远投中的。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("外线连续胯下运球摆脱");
                    builder.Append(def);
                    builder.Append("的防守，突然出手三分球进。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("外线投中三分。");
                    break;
            }
            builder.Append("&lt;/font&gt;");
            return builder.ToString();
        }

        public static string GoodOffenseThreeWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 13);
            StringBuilder builder = new StringBuilder();
            builder.Append("&lt;font color = blue&gt;[三分]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("的传球后，没有运球，直接出手投中一个三分。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("零度角命中一个三分球，是");
                    builder.Append(pass);
                    builder.Append("传出的好球。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("圈顶发炮，三分进了，没有浪费");
                    builder.Append(pass);
                    builder.Append("突破后的助攻。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("的传球，三分球出手命中。");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("助攻");
                    builder.Append(off);
                    builder.Append("三分远投中的。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass + "的传球，稳稳的投中三分。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("晃开在");
                    builder.Append(def);
                    builder.Append("的防守，接");
                    builder.Append(pass);
                    builder.Append("的传球三分远投中的。");
                    break;

                case 7:
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("传球，");
                    builder.Append(off);
                    builder.Append("外线投中三分。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("右翼接");
                    builder.Append(pass);
                    builder.Append("传球射中三分。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("左翼接");
                    builder.Append(pass);
                    builder.Append("传球射中三分。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("外线空档接");
                    builder.Append(pass);
                    builder.Append("传球投中三分。");
                    break;

                case 11:
                    builder.Append(pass);
                    builder.Append("越过");
                    builder.Append(def);
                    builder.Append("的防守助攻");
                    builder.Append(off);
                    builder.Append("投中三分。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("零度角接");
                    builder.Append(pass);
                    builder.Append("传球投进三分。");
                    break;
            }
            builder.Append("&lt;/font&gt;");
            return builder.ToString();
        }

        public static string HighLightBlue(string line)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("&lt;font color = blue&gt;");
            builder.Append(line);
            builder.Append("&lt;/font&gt;");
            return builder.ToString();
        }

        public static string HighLightRed(string line)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("&lt;font color = red&gt;");
            builder.Append(line);
            builder.Append("&lt;/font&gt;");
            return builder.ToString();
        }

        public static string OffFoul(string off, string def)
        {
            int num = rnd.Next(0, 15);
            StringBuilder builder = new StringBuilder();
            builder.Append("[犯规]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("撞人犯规。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("进攻犯规。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("压人犯规。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("篮下抢位时推搡");
                    builder.Append(def);
                    builder.Append("进攻犯规。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("带球撞人。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("肘击");
                    builder.Append(def);
                    builder.Append("进攻犯规。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("转身过人时，手部有个扒人动作。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("强打篮下时，肩部撞人。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("三步上篮，撞倒");
                    builder.Append(def);
                    builder.Append("，进攻犯规。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("一只手运球，另一只手推搡");
                    builder.Append(def);
                    builder.Append(off);
                    builder.Append("犯规。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("变向运球，将");
                    builder.Append(def);
                    builder.Append("撞倒，");
                    builder.Append(off);
                    builder.Append("犯规了。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("挡拆中对");
                    builder.Append(def);
                    builder.Append("犯规。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("抢篮板时侵犯");
                    builder.Append(def);
                    builder.Append("。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("硬闯禁区，结果将已经站好位置的");
                    builder.Append(def);
                    builder.Append("撞倒，");
                    builder.Append(off);
                    builder.Append("进攻犯规。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("进攻中撞人。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("转身时拨住");
                    builder.Append(def);
                    builder.Append("进攻犯规。");
                    break;

                case 0x10:
                    builder.Append(def);
                    builder.Append("站位及时，造成");
                    builder.Append(off);
                    builder.Append("撞人犯规。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("丢球后拉人犯规。");
                    break;
            }
            return builder.ToString();
        }

        public static string OffRebound(string offReb)
        {
            int num = rnd.Next(0, 20);
            StringBuilder builder = new StringBuilder();
            builder.Append("[篮板]");
            switch (num)
            {
                case 0:
                    builder.Append(offReb);
                    builder.Append("抢到篮板！");
                    break;

                case 1:
                    builder.Append(offReb);
                    builder.Append("判断到球的落点，抢到进攻篮板。");
                    break;

                case 2:
                    builder.Append(offReb);
                    builder.Append("高高跳起摘下进攻篮板。");
                    break;

                case 3:
                    builder.Append(offReb);
                    builder.Append("捡到一个进攻篮板。");
                    break;

                case 4:
                    builder.Append(offReb);
                    builder.Append("从人群中跃起，将篮板抱在怀里。");
                    break;

                case 5:
                    builder.Append(offReb);
                    builder.Append("冲抢到一个进攻篮板。");
                    break;

                case 6:
                    builder.Append("篮板被");
                    builder.Append(offReb);
                    builder.Append("抢到。");
                    break;

                case 7:
                    builder.Append(offReb);
                    builder.Append("起跳将球挑出来，再次跳起把球抓到手里。");
                    break;

                case 8:
                    builder.Append(offReb);
                    builder.Append("抢到进攻篮板。");
                    break;

                case 9:
                    builder.Append(offReb);
                    builder.Append("捡了一个进攻篮板。");
                    break;

                case 10:
                    builder.Append(offReb);
                    builder.Append("在内线占到位置，争抢到这次的进攻篮板。");
                    break;

                case 11:
                    builder.Append(offReb);
                    builder.Append("对胜利的渴望的确惊人，又一次抢到进攻篮板。");
                    break;

                case 12:
                    builder.Append(offReb);
                    builder.Append("抢下进攻篮板。");
                    break;

                case 13:
                    builder.Append("成功倚住对手，");
                    builder.Append(offReb);
                    builder.Append("抢到进攻篮板。");
                    break;

                case 14:
                    builder.Append(offReb);
                    builder.Append("连续起跳，抢到进攻篮板。");
                    break;

                case 15:
                    builder.Append(offReb);
                    builder.Append("从对方球员头顶抢下进攻篮板。");
                    break;

                case 0x10:
                    builder.Append(offReb);
                    builder.Append("绕前卡位，提前起跳，抢到进攻篮板。");
                    break;

                case 0x11:
                    builder.Append(offReb);
                    builder.Append("冲到篮下，高高跃起抓下篮板。");
                    break;

                case 0x12:
                    builder.Append(offReb);
                    builder.Append("从对方手中抢到篮板。");
                    break;

                case 0x13:
                    builder.Append(offReb);
                    builder.Append("伸手拿下队友拨过来的篮球。");
                    break;
            }
            return builder.ToString();
        }

        public static string Steal(string off, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[断球]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("运球突破时，");
                    builder.Append(def);
                    builder.Append("断球成功。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("传球，被");
                    builder.Append(def);
                    builder.Append("断了。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("用不习惯的左手运球，果然被");
                    builder.Append(def);
                    builder.Append("断下。");
                    break;

                case 3:
                    builder.Append(def);
                    builder.Append("突然绕前，断下");
                    builder.Append(off);
                    builder.Append("的传球。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("传球被");
                    builder.Append(def);
                    builder.Append("看穿意图，把球断下。");
                    break;

                case 5:
                    builder.Append(def);
                    builder.Append("压低重心，伸手断下");
                    builder.Append(off);
                    builder.Append("的皮球。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("跨下运球过人，被");
                    builder.Append(def);
                    builder.Append("轻松断下。");
                    break;

                case 7:
                    builder.Append(def);
                    builder.Append("死缠烂打，疯狂的防守中断下");
                    builder.Append(off);
                    builder.Append("的皮球。");
                    break;

                case 8:
                    builder.Append(def);
                    builder.Append("攻击性防守，迫使");
                    builder.Append(off);
                    builder.Append("运球失误，球被");
                    builder.Append(def);
                    builder.Append("抢下。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("运球变向，");
                    builder.Append(def);
                    builder.Append("看穿球路，直接断下皮球。");
                    break;
            }
            return builder.ToString();
        }

        public static string TO(string off, string def)
        {
            int num = rnd.Next(0, 9);
            StringBuilder builder = new StringBuilder();
            builder.Append("[失误]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("走步违例！");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("时，球运出底线！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("忙中出错，踢球违例！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("两次运球违例！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("球投到篮板后沿违例！");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("时，脚踩底线！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("3秒违例！");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("时，带球走步违例！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("掷界外球超过5秒钟违例！");
                    break;
            }
            return builder.ToString();
        }

        public static string Warning()
        {
            return "错误警告！！";
        }
    }
}

