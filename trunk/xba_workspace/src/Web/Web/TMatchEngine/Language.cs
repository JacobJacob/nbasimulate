namespace Web.TMatchEngine
{
    using System;
    using System.Text;

    public class Language
    {
        public static Random rnd = new Random(DateTime.Now.Millisecond);

        public static string BadOffenseBreak(string off, string pass, string def)
        {
            int num = rnd.Next(0, 20);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("三步上篮，空中被");
                    builder.Append(def);
                    builder.Append("撞了一下，球没进！");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("篮下强攻，遭");
                    builder.Append(def);
                    builder.Append("奋力封盖，球没进。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("突破上篮，没进。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("强行上篮，被");
                    builder.Append(def);
                    builder.Append("的防守干扰。");
                    break;

                case 4:
                    builder.Append(def);
                    builder.Append("死防");
                    builder.Append(off);
                    builder.Append("突破，");
                    builder.Append(off);
                    builder.Append("无奈跳投，不中。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("突破，虚晃，起跳，投篮，不中，可惜！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("突破上篮，不进！");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("上篮，");
                    builder.Append(def);
                    builder.Append("及时封盖，球没进。");
                    break;

                case 8:
                    builder.Append(def);
                    builder.Append("防守对");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("的突破没有形成任何威胁。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("右路突破，钩手，不进。");
                    break;

                case 10:
                    builder.Append(def);
                    builder.Append("背后干扰");
                    builder.Append(off);
                    builder.Append("的上篮，球没进。");
                    break;

                case 11:
                    builder.Append(def);
                    builder.Append("的防守下，");
                    builder.Append(off);
                    builder.Append("强行突破，跳投，不中。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("突破上篮不进。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("突破后，打板不中。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("运球突破，投篮不中。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("突破欲造");
                    builder.Append(def);
                    builder.Append("犯规，球没进。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("上篮，不中。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("突破2名防守队员得夹击防守，挑篮没进，可惜！");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("突破，拉杆，球打板不中。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("左路突破，钩手，不中。");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseBreakBlock(string off, string def)
        {
            int num = rnd.Next(0, 14);
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

                case 11:
                    builder.Append(off);
                    builder.Append("突破上篮！哦！被");
                    builder.Append(def);
                    builder.Append("一个大帽盖了下来！");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("高高跃起，大力扣篮，被");
                    builder.Append(def);
                    builder.Append("连人带球扇了下来！");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("快攻是形成前场一对一，可惜上篮被");
                    builder.Append(def);
                    builder.Append("盖了出去！");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string BadOffenseFast(string off, string pass, string def)
        {
            int num = rnd.Next(0, 11);
            StringBuilder builder = new StringBuilder();
            builder.Append("[快攻]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("运球到外线，投篮不中。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("单手扣篮，竟然没进。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("双手扣篮，球砸在篮脖子上飞了。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("上篮时脚步一乱，没有投中。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("接到长传，跳投不中。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("挑篮不中。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("接到" + pass + "传球挑篮不中。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("前场接到");
                    builder.Append(pass);
                    builder.Append("长传球单手扣篮，球砸在篮脖子上飞了。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("传球，上篮，球涮筐而出。");
                    break;

                case 9:
                    builder.Append(pass);
                    builder.Append("长传至前场，");
                    builder.Append(off);
                    builder.Append("跳投不中。");
                    break;

                case 10:
                    builder.Append(pass);
                    builder.Append("长传至前场，");
                    builder.Append(off);
                    builder.Append("外线接球出手，不中！");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseInside(string off, string pass, string def)
        {
            int num = rnd.Next(0, 13);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("强打内线，钩手，打板不进。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("内线跑出空挡，接球投篮，没进。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("面对对方中锋防守，无奈钩手，不中。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("突入内线，挑篮，被");
                    builder.Append(def);
                    builder.Append("干扰，不中。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("溜底线，翻篮，不中。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("得");
                    builder.Append(pass);
                    builder.Append("传球，单手扣篮。球砸筐偏出。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("倚到内线，钩手，打板，不中。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("内线转身跳投，被防守干扰，三不沾。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("45度角钩手，不中。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("0度角钩手，AIR BALL！");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("90度角钩手，偏出。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append(" 空切内线，接球挑篮，球滑筐而出。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("强打内线，投篮被");
                    builder.Append(def);
                    builder.Append("干扰。");
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
                    builder.Append("的底线勾手！");
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
                    builder.Append("篮底硬挤，刚出手就被");
                    builder.Append(def);
                    builder.Append("扇了下来。");
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
            int num = rnd.Next(0, 13);
            StringBuilder builder = new StringBuilder();
            builder.Append("[挡拆]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("和队友做了个档拆，空挡出手，不进。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("为");
                    builder.Append(off);
                    builder.Append("挡出空当，");
                    builder.Append(off);
                    builder.Append("跳投，不中。");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("与");
                    builder.Append(off);
                    builder.Append("做了个挡拆配合");
                    builder.Append(off);
                    builder.Append("上篮被");
                    builder.Append(def);
                    builder.Append("干扰。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("投篮不进。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("上篮不进。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("挑篮不进。");
                    break;

                case 6:
                    builder.Append("挡拆配合后，");
                    builder.Append(off);
                    builder.Append("投篮不进。");
                    break;

                case 7:
                    builder.Append("挡拆配合后，");
                    builder.Append(off);
                    builder.Append("上篮不进。");
                    break;

                case 8:
                    builder.Append("挡拆配合后，");
                    builder.Append(off);
                    builder.Append("挑篮不进。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("与");
                    builder.Append(pass);
                    builder.Append("做了个挡拆配合");
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("上篮未果。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("与");
                    builder.Append(pass);
                    builder.Append("的挡拆配合没有甩掉防守，");
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("防守下，投篮，AIR BALL！");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("与一名无球球员挡拆，跑出空挡，得");
                    builder.Append(pass);
                    builder.Append("的传球，中投，竟然是个三不沾！");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("和");
                    builder.Append(pass);
                    builder.Append("做了一个简单的挡拆，");
                    builder.Append(off);
                    builder.Append("中投，没进。");
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
            return HighLightBlue(builder.ToString());
        }

        public static string BadOffenseShot(string off, string pass, string def)
        {
            int num = rnd.Next(0, 0x15);
            StringBuilder builder = new StringBuilder();
            builder.Append("[外线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("急停跳投，AIR BALL！");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("匆忙出手，没进。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("后仰投篮，没进。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("假动作晃过");
                    builder.Append(def);
                    builder.Append("的防守，投篮没进。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("0度角投篮，打在篮板上，没进。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("投篮不中。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("中投没进。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("转身投篮，偏出篮筐。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("45度角后仰跳投，没进。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("0度角后仰跳投，打板不中。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("90度角后仰跳投，在严密防守下，球还是没进。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("假动作骗过");
                    builder.Append(def);
                    builder.Append("，跳投，没进。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("45度角强行出手，没进。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("90度角强行出手，不中。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("跑出空挡，三分线内接球投篮，偏出不少。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("急停跳投，防守没有跟上，球卡在篮框上，不中！");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("胯下运球变向，提速，跳投，不中！可惜！");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("假动作晃起防守球员，跳投，不中！");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("后撤一步，随手就投，AIR BALL！");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("罚球线附近跳投，不中！有失水准！");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("0度角强行出手，被");
                    builder.Append(def);
                    builder.Append("干扰，AIR BALL！");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseShotBlock(string off, string def)
        {
            int num = rnd.Next(0, 12);
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

                case 7:
                    builder.Append(off);
                    builder.Append("跳投，被");
                    builder.Append(def);
                    builder.Append("给盖了下来！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("边角跳投，被");
                    builder.Append(def);
                    builder.Append("一巴掌扇在篮板上！惨！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("钩手投篮，竟被");
                    builder.Append(def);
                    builder.Append("轻轻盖了一下。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("的投篮被");
                    builder.Append(def);
                    builder.Append("扇飞了。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("跳投，被");
                    builder.Append(def);
                    builder.Append("盖了下来。");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string BadOffenseThree(string off, string pass, string def)
        {
            int num = rnd.Next(0, 0x12);
            StringBuilder builder = new StringBuilder();
            builder.Append("[三分]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("跑出三分空挡，投篮没进。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("施射三分，砸在篮板上！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("施射三分，球打在篮筐上弹了出来。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("三分线外拿球便投，没进！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("跑出空档，接同伴传球，三分没进！");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("做了个欲传内线的假动作，突然施射三分，没进！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("接到皮球，向后退了一步到三分线外便射，三分不中！");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("45度角发炮，不中！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("0度角发炮，碰在篮圈上弹出！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("90度角发炮，正卡在篮板上弹飞！");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("在对方严密防守下投射三分，不中！");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("跳投三分，打板弹出。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("三分线外无人防守，出手不中。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("混战中抢得皮球，匆忙中射三分，没进！");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("接内线传球，射三分，不进");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("与队友单挡出空间，三分不中！");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("三分线外，边角发炮，擦板不进！");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("三分线外出手，没进！");
                    break;
            }
            return builder.ToString();
        }

        public static string BadOffenseThreeBlock(string off, string def)
        {
            int num = rnd.Next(0, 9);
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

                case 8:
                    builder.Append(off);
                    builder.Append("射三分，");
                    builder.Append(def);
                    builder.Append("补防及时，给帽了出去！");
                    break;
            }
            return builder.ToString();
        }

        public static string DefFoul(string off, string def)
        {
            int num = rnd.Next(0, 0x15);
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

                case 15:
                    builder.Append(off);
                    builder.Append("造成对方犯规。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("接球时造成对方犯规。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("造成");
                    builder.Append(def);
                    builder.Append("犯规。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("运球推进，造成");
                    builder.Append(def);
                    builder.Append("犯规。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("突内线故意造成");
                    builder.Append(def);
                    builder.Append("犯规。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("强突内线造成对方");
                    builder.Append(def);
                    builder.Append("犯规。");
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

        public static string GoodOffenseBreak(string off, string def)
        {
            int num = rnd.Next(0, 0x25);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("突破后抛投得手。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("突破后打板进球。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("底线进攻，上篮得分。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("突破上篮，把球送入篮筐。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("反手上篮命中。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("转身勾手中的。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("单打");
                    builder.Append(def);
                    builder.Append("，勾手投中一球。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("底线转身，上篮得分。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("的防守，空档处投篮得分。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("突破中顶住");
                    builder.Append(def);
                    builder.Append("的贴身防守，突然一个后仰投篮，篮球空心入网。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("突破后，跑投得手。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("突破得分。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("突破防守，挑篮得分。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("转身，摆脱");
                    builder.Append(def);
                    builder.Append("，上篮得分。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("三步上篮，将");
                    builder.Append(def);
                    builder.Append("甩到身后投篮得分。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("连续胯下运球，晃过");
                    builder.Append(def);
                    builder.Append("的防守，高手上篮得分。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("连续胯下运球，晃过" + def + "的防守，低手上篮得分。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("三秒区外突然一个变向三步上篮得手。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("冲到禁区左勾手打板得分。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("转身突破了");
                    builder.Append(def);
                    builder.Append("的防守，左手上篮得分。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("胯下运球，过了防守者");
                    builder.Append(def);
                    builder.Append("，突到内线上篮拿分。");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("起三步侧勾得手。");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("猛地一个转身过了");
                    builder.Append(def);
                    builder.Append("，打板得分。");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("运球左右晃动，突破了");
                    builder.Append(def);
                    builder.Append("防守抛投得分。");
                    break;

                case 0x18:
                    builder.Append(off);
                    builder.Append("起步，顶住");
                    builder.Append(def);
                    builder.Append("贴身防守侧勾得手。");
                    break;

                case 0x19:
                    builder.Append(off);
                    builder.Append("内线拿球胯下转身，后仰投篮命中。");
                    break;

                case 0x1a:
                    builder.Append(off);
                    builder.Append("突破，急停跳投命中。");
                    break;

                case 0x1b:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("的防守，绕篮筐转圈，侧身勾手打板把球投进。");
                    break;

                case 0x1c:
                    builder.Append(off);
                    builder.Append("罚球线冲到篮底，正面打板球进。");
                    break;

                case 0x1d:
                    builder.Append(off);
                    builder.Append("胯下运球，转身勾手投进。");
                    break;

                case 30:
                    builder.Append(off);
                    builder.Append("突到篮下，举球虚晃过" + def + "后打板得分。");
                    break;

                case 0x1f:
                    builder.Append(off);
                    builder.Append("快速右转身，空中出手命中。");
                    break;

                case 0x20:
                    builder.Append(off);
                    builder.Append("右手胯下运球，换左手持球冲到篮底，勾手打板球进。");
                    break;

                case 0x21:
                    builder.Append(off);
                    builder.Append("胯下运球，突破了");
                    builder.Append(def);
                    builder.Append("的防守，背对篮筐勾手得分。");
                    break;

                case 0x22:
                    builder.Append("看到");
                    builder.Append(def);
                    builder.Append("侧对着篮筐露出防守漏洞，");
                    builder.Append(off);
                    builder.Append("一个跨步就把");
                    builder.Append(def);
                    builder.Append("甩到身后，反手上篮得分。");
                    break;

                case 0x23:
                    builder.Append(off);
                    builder.Append("连续的运球晃动，最后冲起来跑投得手。");
                    break;

                case 0x24:
                    builder.Append(off);
                    builder.Append("强行起三步挤进内线，勾手命中。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseBreakDunk(string off, string def)
        {
            int num = rnd.Next(0, 20);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("快速的胯下运球，晃晕了");
                    builder.Append(def);
                    builder.Append("，突到内线扣篮！");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("急停，加速，过了重心没调整好的");
                    builder.Append(def);
                    builder.Append("，轻松抛投得手！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("突向左侧，突然变向杀往右方，");
                    builder.Append(def);
                    builder.Append("晃倒在地，");
                    builder.Append(off);
                    builder.Append("轻松单手扣篮！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("提速，冲到禁区，飞身扣篮！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("持球在三秒区外游走，突然一个变向三步上篮得手！");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("奋力起步，顶住");
                    builder.Append(def);
                    builder.Append("贴身防守侧勾得手！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("快速的左右晃动，一个大幅度转身起跳，双手大灌篮！");
                    break;

                case 7:
                    builder.Append(def);
                    builder.Append("伸手掏球，");
                    builder.Append(off);
                    builder.Append("猛地一个转身过了");
                    builder.Append(def);
                    builder.Append("，举手便扣，毫不客气！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("拿球往左一晃，然后一个快速的右转身，摆脱防守，举手轻松扣篮。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("连续的运球右晃，突然冲起来压着");
                    builder.Append(def);
                    builder.Append("的头顶，来了记重扣！侮辱！");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("加速突到篮下，举球虚晃过");
                    builder.Append(def);
                    builder.Append("打板得分！");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("底线突破，急转身，零度角单手扣中！");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("胯下运球，大幅转身勾手投进！");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("右手一次胯下运球变向，左手持球高速冲到篮底，勾手打板球进！");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("运球突破，突然后转身后仰投篮命中！");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("从罚球线全速冲向篮底，正面打板球进！");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("“三分线”外一溜烟杀到篮底，空中闪过防守将球投进！");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("的防守，高速绕篮筐转了一个半圆，最后侧身勾手打板把球投进！");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("加速突破");
                    builder.Append(def);
                    builder.Append("的防守，揉身将球投进篮筐！");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("突到内线，突然胯下转身，后仰投篮命中！");
                    break;
            }
            return HighLightBlue(builder.ToString());
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
                    builder.Append(off + "，后者上篮得分。");
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
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffenseBreakGoodDunk(string off, string def)
        {
            int num = rnd.Next(0, 0x25);
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
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("的防守，以一个大风车灌篮结束本次进攻！");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("带球转身，过了");
                    builder.Append(def);
                    builder.Append("顺势扣了一个！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("胯下运球，突破了");
                    builder.Append(def);
                    builder.Append("的防守，背对篮筐扣篮得分！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("的防守，背对篮筐反身扣篮！");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("将球抛向篮板，飞身空中接球一个暴扣！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("一个假动作，晃过");
                    builder.Append(def);
                    builder.Append("的防守大力扣篮得分！");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("的防守，高高跃起，一个反手扣篮！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("的防守单手持球起跳，背对篮筐曲膝劈腿反身扣篮！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("的防守面对篮筐双手持球，抡了个大风车，单手扣篮！");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("胯下运球转身将");
                    builder.Append(def);
                    builder.Append("甩到身后，面对篮筐扣篮得分！");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("的防守，面对篮筐滑行后转身，双手从脑后扣篮！");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("左手一次胯下，球转到右手转身摆脱了");
                    builder.Append(def);
                    builder.Append("的防守，横空一个侧扣！");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("的防守，面对篮筐单手扣篮！");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("单手挟球起跳，180度转身双手扣篮！");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("的防守，单手挟球起跳，左手大风车扣篮！");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("带球冲过");
                    builder.Append(def);
                    builder.Append("的左侧，单手挟球起跳，空中左手拉杆扣篮！");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("突破，双手灌篮得分！");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("的防守，来了一个大风车扣篮！");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("防守后右手扣篮！");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("突破");
                    builder.Append(def);
                    builder.Append("防守后左手扣篮！");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("全速带球面对身前的");
                    builder.Append(def);
                    builder.Append("跃起扣篮！");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("空中躲过");
                    builder.Append(def);
                    builder.Append("封盖，拉杆扣篮！");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("飞越");
                    builder.Append(def);
                    builder.Append("头顶，完美的一次扣篮！");
                    break;

                case 0x18:
                    builder.Append(off);
                    builder.Append("背后运球摆脱");
                    builder.Append(def);
                    builder.Append("的防守，扣篮得分！");
                    break;

                case 0x19:
                    builder.Append(off);
                    builder.Append("急停，突然又一个加速，过了重心没调整好的");
                    builder.Append(def);
                    builder.Append("，轻松将球放进篮筐！");
                    break;

                case 0x1a:
                    builder.Append(off);
                    builder.Append("一个体前运球，顺势转身，又一个体前变向运球，将");
                    builder.Append(def);
                    builder.Append("晃傻了，从左侧冲到禁区扣篮！");
                    break;

                case 0x1b:
                    builder.Append(off);
                    builder.Append("突破后，篮下轻松扣篮！");
                    break;

                case 0x1c:
                    builder.Append(off);
                    builder.Append("突然一个起步将");
                    builder.Append(def);
                    builder.Append("甩到身后灌篮！");
                    break;

                case 0x1d:
                    builder.Append(off);
                    builder.Append("一个加速，突破了");
                    builder.Append(def);
                    builder.Append("的防守，双手扣篮！");
                    break;

                case 30:
                    builder.Append(off);
                    builder.Append("胯下运球，顺势一个快速转身，左手将球灌进篮筐！");
                    break;

                case 0x1f:
                    builder.Append(off);
                    builder.Append("拿球背对");
                    builder.Append(def);
                    builder.Append("，突然一个前转身溜进了底线双手反灌！");
                    break;

                case 0x20:
                    builder.Append(off);
                    builder.Append("连续的胯下运球，晃晕了防守者");
                    builder.Append(def);
                    builder.Append("，起三步，单手将球扣进篮筐！");
                    break;

                case 0x21:
                    builder.Append(off);
                    builder.Append("一个转身，变向运球，突破了");
                    builder.Append(def);
                    builder.Append("的防守，左手扣篮！");
                    break;

                case 0x22:
                    builder.Append(off);
                    builder.Append("突破的第一步太快了，");
                    builder.Append(def);
                    builder.Append("的防守型同虚设，无奈得看着");
                    builder.Append(off);
                    builder.Append("灌篮的身影！");
                    break;

                case 0x23:
                    builder.Append(def);
                    builder.Append("侧对着篮筐，伸手防守，");
                    builder.Append(off);
                    builder.Append("一个跨步就把");
                    builder.Append(def);
                    builder.Append("甩到身后，反手扣篮！");
                    break;

                case 0x24:
                    builder.Append(off);
                    builder.Append("突向左侧，突然变向杀往右方，将");
                    builder.Append(def);
                    builder.Append("晃倒在地，双手暴扣！");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffenseBreakGoodDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 9);
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
                    builder.Append(off);
                    builder.Append("甩开");
                    builder.Append(def);
                    builder.Append("，突然一个前转身溜进了底线接接");
                    builder.Append(pass);
                    builder.Append("恰到好处的传球，扣篮！");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("助攻，");
                    builder.Append(off);
                    builder.Append("一个加速，突破了");
                    builder.Append(def);
                    builder.Append("的防守，挑篮得手！");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffenseBreakWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 0x1a);
            StringBuilder builder = new StringBuilder();
            builder.Append("[突破]");
            switch (num)
            {
                case 0:
                    builder.Append(pass);
                    builder.Append("传球给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("佯装投篮晃过防守，抛投得手。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，突破后打板进球。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("底线得球，直接上篮得分。");
                    builder.Append(pass);
                    builder.Append("助攻一次。");
                    break;

                case 3:
                    builder.Append(pass);
                    builder.Append("突破分球，");
                    builder.Append(off);
                    builder.Append("接球上篮，把球轻松送入篮筐。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("底线接到队友妙传，反手上篮命中。");
                    builder.Append(pass);
                    builder.Append("助攻。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("转身勾手中的。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球后，突破" + def + "后打板进球。");
                    break;

                case 7:
                    builder.Append(pass);
                    builder.Append("传球给底线的");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("底线突破，上篮得分。");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("突破分球，");
                    builder.Append(off);
                    builder.Append("单打");
                    builder.Append(def);
                    builder.Append("，勾手投中。");
                    break;

                case 9:
                    builder.Append(pass);
                    builder.Append("突破后，传球给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("抛投得手。");
                    break;

                case 10:
                    builder.Append(pass);
                    builder.Append("转身，摆脱");
                    builder.Append(def);
                    builder.Append("，传球给无人防守的");
                    builder.Append(off);
                    builder.Append("上篮得分。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，晃过");
                    builder.Append(def);
                    builder.Append("的防守，低手上篮得分。");
                    break;

                case 12:
                    builder.Append(pass);
                    builder.Append("突破未果，把球传出，");
                    builder.Append(off);
                    builder.Append("转身过了");
                    builder.Append(def);
                    builder.Append("，打板得分。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("接过");
                    builder.Append(pass);
                    builder.Append("的传球，顺势跑投得手。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("空切内线接");
                    builder.Append(pass);
                    builder.Append("传球，后仰投篮命中。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("的击地传球，突破起跳，空中躲过");
                    builder.Append(def);
                    builder.Append("封盖，打板得分。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("胯下运球，突然一个起步将");
                    builder.Append(def);
                    builder.Append("甩到身后上篮得分！");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("拿球做了一个左边突破的假动作，骗得");
                    builder.Append(def);
                    builder.Append("重心偏移后，高速从右侧越过，篮下打板得分！");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("做了个左侧突破的假动作，骗开");
                    builder.Append(def);
                    builder.Append("防守，高速从右侧上篮得分！");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("胯下运球，顺势一个转身，左手勾手篮球进筐！");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("跨步上篮，");
                    builder.Append(def);
                    builder.Append("贴身紧逼，");
                    builder.Append(off);
                    builder.Append("从篮板另一侧将球勾进！");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("一个加速，突破了");
                    builder.Append(def);
                    builder.Append("的防守，挑篮得手！");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("快速上篮得手！");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("一个快速的转身，换手，突破了");
                    builder.Append(def);
                    builder.Append("的防守，左手上篮得分！");
                    break;

                case 0x18:
                    builder.Append(off);
                    builder.Append("突破的第一步太快了，");
                    builder.Append(def);
                    builder.Append("的防守型同虚设，只能眼看着");
                    builder.Append(off);
                    builder.Append("上篮得分！");
                    break;

                case 0x19:
                    builder.Append(off);
                    builder.Append("拿球背对");
                    builder.Append(def);
                    builder.Append("，突然一个前转身溜进了底线挑篮中的！");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseFast(string off, string def)
        {
            int num = rnd.Next(0, 12);
            StringBuilder builder = new StringBuilder();
            builder.Append("[快攻]");
            switch (num)
            {
                case 0:
                    builder.Append("快攻得手。前场三打一，由");
                    builder.Append(off);
                    builder.Append("轻松上篮得分。");
                    break;

                case 1:
                    builder.Append("前场二打一，");
                    builder.Append(off);
                    builder.Append("假动作传球队友，晃开");
                    builder.Append(def);
                    builder.Append("轻松上篮。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("快速突破，用速度过了");
                    builder.Append(def);
                    builder.Append("，直接上篮。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("单人快功，前场一打二，突破");
                    builder.Append(def);
                    builder.Append("，跳投得分。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("快功中，突破最后一名防守");
                    builder.Append(def);
                    builder.Append("，轻松挑篮得分。");
                    break;

                case 5:
                    builder.Append("拿球立即快攻反击，三传两递到前场");
                    builder.Append(off);
                    builder.Append("跑空篮，拿下两分。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("抓住反击机会，快速通过前场，并背后运球骗过");
                    builder.Append(def);
                    builder.Append("，上篮得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("前场接到队友及时的长传，直接上篮得分。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("前场接球，骗过");
                    builder.Append(def);
                    builder.Append("的防守，挑篮得分。");
                    break;

                case 9:
                    builder.Append("快攻至前场，三打二，");
                    builder.Append(off);
                    builder.Append("无人防守，得球轻松中投二分。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("快攻得手。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("快攻，上了个空篮，得2分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseFastDunk(string off, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[快攻]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("快攻得手，单手大力扣篮得分。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("快攻得手，双手灌篮。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("快攻得手，上篮得分。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("得到快攻机会，挑篮得分。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("以速度突破，单手扣篮。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("单人快功，利用熟练的运球骗过两名防守，上篮得分。");
                    break;

                case 6:
                    builder.Append("两人快功。");
                    builder.Append(off);
                    builder.Append("底线得球，起步，扣篮！流畅！");
                    break;

                case 7:
                    builder.Append("快攻推进，三传两递，球到前场，");
                    builder.Append(off);
                    builder.Append("拿球面对");
                    builder.Append(def);
                    builder.Append("高手上篮得分。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("得球，提速，全场一条龙，扣篮得分！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("接球快攻，只有");
                    builder.Append(def);
                    builder.Append("一人回防，");
                    builder.Append(off);
                    builder.Append("轻松突破，扣篮！");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffenseFastDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[快攻]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("传球单手扣篮得分。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("长传至前场，");
                    builder.Append(off);
                    builder.Append("双手扣篮得分。");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("长传至前场，");
                    builder.Append(off);
                    builder.Append("单手扣篮得分。");
                    break;

                case 3:
                    builder.Append(pass);
                    builder.Append("长传至前场，");
                    builder.Append(off);
                    builder.Append("双手扣篮得分。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("传球双手扣篮得分。");
                    break;

                case 5:
                    builder.Append(pass);
                    builder.Append("传给");
                    builder.Append(off);
                    builder.Append(",");
                    builder.Append(off);
                    builder.Append("背后运球骗过");
                    builder.Append(def);
                    builder.Append("，起步扣篮。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("迅速下底，接");
                    builder.Append(pass);
                    builder.Append("长传，底线突破");
                    builder.Append(def);
                    builder.Append("，扣篮。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("底线");
                    builder.Append(pass);
                    builder.Append("及时的长传，无人防守，扣篮得分。");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("策动快攻反击，");
                    builder.Append(off);
                    builder.Append("接球，起步，在");
                    builder.Append(def);
                    builder.Append("头上来了个头顶开花！");
                    break;

                case 9:
                    builder.Append("前场三打一，");
                    builder.Append(off);
                    builder.Append("无人防守，得");
                    builder.Append("跑空篮得手。");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffenseFastWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[快攻]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("传球挑篮得分。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("传球低手上篮。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("传球上篮得分。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("得到快攻机会，轻松摘得2分。");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("长传至前场，");
                    builder.Append(off);
                    builder.Append("轻松摘得2分。");
                    break;

                case 5:
                    builder.Append(pass);
                    builder.Append("传给");
                    builder.Append(off);
                    builder.Append(",");
                    builder.Append(off);
                    builder.Append("上篮得分。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("抓住反击机会，快速通过前场，接");
                    builder.Append(pass);
                    builder.Append("长传，上篮得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("前场接到");
                    builder.Append(pass);
                    builder.Append("及时的长传，直接上篮得分。");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("策动快攻反击，迅速通过前场，传");
                    builder.Append(off);
                    builder.Append("，后者直接上篮。轻松的二分。");
                    break;

                case 9:
                    builder.Append("快攻至前场，三打一，");
                    builder.Append(off);
                    builder.Append("无人防守，得");
                    builder.Append(pass);
                    builder.Append("传球轻松中投二分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseInside(string off, string def)
        {
            int num = rnd.Next(0, 0x3d);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("内线强打，零度角跳投得分。");
                    break;

                case 1:
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("防守，");
                    builder.Append(off);
                    builder.Append("内线强打得分。");
                    break;

                case 2:
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("防守，");
                    builder.Append(off);
                    builder.Append("内线强打，");
                    builder.Append("45度角跳投得分。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("内线强打，45度角跳投得分。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("强打内线，做了个左转身的假动作，骗开");
                    builder.Append(def);
                    builder.Append("，右转身轻松投中1球。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("做了个右转身的假动作，骗开");
                    builder.Append(def);
                    builder.Append("，左转身轻松投中1球。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("篮下跳投得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("篮下勾手得分。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("倚到内线，打板得分。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("篮下得分。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("转身出手得分。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("内线投篮得分。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("转身跳投得分。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("内线跳投得分。");
                    break;

                case 14:
                    builder.Append(def);
                    builder.Append("内线防守失位，");
                    builder.Append(off);
                    builder.Append("轻松投中。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("罚球线处勾手命中。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("内线单打");
                    builder.Append(def);
                    builder.Append("得分。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("篮下找到空档后命中。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("篮下一个转身，甩掉");
                    builder.Append(def);
                    builder.Append("的防守，跳投得分。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("篮下投篮，球在篮筐上转了几圈，还是进了。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("内线转身跳投2分。");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("篮下跳投拿下2分。");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("篮下跳投，");
                    builder.Append(def);
                    builder.Append("防守不及，拿下2分。");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("篮下打板进球。");
                    break;

                case 0x18:
                    builder.Append(off);
                    builder.Append("内线射中一球。");
                    break;

                case 0x19:
                    builder.Append(off);
                    builder.Append("内线半勾手射中一球。");
                    break;

                case 0x1a:
                    builder.Append(off);
                    builder.Append("从底线进攻，上篮得分，");
                    builder.Append(def);
                    builder.Append("防守无济于事。");
                    break;

                case 0x1b:
                    builder.Append(off);
                    builder.Append("运球到了篮下，在");
                    builder.Append(def);
                    builder.Append("头顶打板命中。");
                    break;

                case 0x1c:
                    builder.Append(off);
                    builder.Append("挤到禁区，在");
                    builder.Append(def);
                    builder.Append("头顶扣篮。");
                    break;

                case 0x1d:
                    builder.Append(off);
                    builder.Append("强打内线，突然一个转身勾手，中了。");
                    break;

                case 30:
                    builder.Append(off);
                    builder.Append("强吃");
                    builder.Append(def);
                    builder.Append("，投中1球。");
                    break;

                case 0x1f:
                    builder.Append(off);
                    builder.Append("运球变向，篮下高手上篮得分。");
                    break;

                case 0x20:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("严防下仓促跳投，球进。");
                    break;

                case 0x21:
                    builder.Append(off);
                    builder.Append("底线转身，上篮得分。");
                    break;

                case 0x22:
                    builder.Append(def);
                    builder.Append("防守严密，突然，");
                    builder.Append(off);
                    builder.Append("内线跳投，");
                    builder.Append(def);
                    builder.Append("的手已摸到了皮球，但球还是进了。");
                    break;

                case 0x23:
                    builder.Append(def);
                    builder.Append("伸手断球，");
                    builder.Append(off);
                    builder.Append("背后运球过了");
                    builder.Append(def);
                    builder.Append("，挑篮得分。");
                    break;

                case 0x24:
                    builder.Append(off);
                    builder.Append("强行挤进禁区，在");
                    builder.Append(def);
                    builder.Append("头顶跳投命中。");
                    break;

                case 0x25:
                    builder.Append(off);
                    builder.Append("用身体顶住");
                    builder.Append(def);
                    builder.Append("，正面勾手投中。");
                    break;

                case 0x26:
                    builder.Append(off);
                    builder.Append("篮下右转身，拉开与");
                    builder.Append(def);
                    builder.Append("的距离，跳投得分。");
                    break;

                case 0x27:
                    builder.Append(off);
                    builder.Append("持球晃过");
                    builder.Append(def);
                    builder.Append("，轻松扣篮。");
                    break;

                case 40:
                    builder.Append(off);
                    builder.Append("做了个右转身的假动作，骗开");
                    builder.Append(def);
                    builder.Append("，左转身轻松投中一球。");
                    break;

                case 0x29:
                    builder.Append(off);
                    builder.Append("往左虚晃");
                    builder.Append(def);
                    builder.Append("，闪到右侧拉出空档中投得手！");
                    break;

                case 0x2a:
                    builder.Append(off);
                    builder.Append("篮板左侧连续跨下运球，突然拔地而起打板得分！");
                    break;

                case 0x2b:
                    builder.Append(off);
                    builder.Append("跑动中后仰跳投命中！");
                    break;

                case 0x2c:
                    builder.Append(off);
                    builder.Append("突破，急停，一个旱地拔葱跳投得分！");
                    break;

                case 0x2d:
                    builder.Append(off);
                    builder.Append("运球连续做了几个假动作，晃偏了");
                    builder.Append(def);
                    builder.Append("的防守重心，稳稳地跳起投篮得分！");
                    break;

                case 0x2e:
                    builder.Append(off);
                    builder.Append("零度角中投命中！");
                    break;

                case 0x2f:
                    builder.Append(off);
                    builder.Append("禁区外背打，大勾手把球投中！明显是蒙的！");
                    break;

                case 0x30:
                    builder.Append(off);
                    builder.Append("罚球线外转身后仰投篮命中！");
                    break;

                case 0x31:
                    builder.Append(def);
                    builder.Append("防守很积极，");
                    builder.Append(off);
                    builder.Append("失去了合适的出手角度，无奈一个大后仰投篮，球居然进了");
                    break;

                case 50:
                    builder.Append(off);
                    builder.Append("弧顶虚晃了一下");
                    builder.Append(def);
                    builder.Append("，远投得手！");
                    break;

                case 0x33:
                    builder.Append(off);
                    builder.Append("控球，");
                    builder.Append(def);
                    builder.Append("防守其快速突破，");
                    builder.Append(off);
                    builder.Append("突然远抛投篮得手！");
                    break;

                case 0x34:
                    builder.Append(off);
                    builder.Append("右手一个跨下运球，转到左手体前变向，突破了");
                    builder.Append(def);
                    builder.Append("的防守，后仰投篮命中！");
                    break;

                case 0x35:
                    builder.Append(off);
                    builder.Append("强行突破");
                    builder.Append(def);
                    builder.Append("，急停中投得手！");
                    break;

                case 0x36:
                    builder.Append(off);
                    builder.Append("拿球单打，一晃45度角打板得分！");
                    break;

                case 0x37:
                    builder.Append(off);
                    builder.Append("胯下运球，一个后撤步，挑投得手！");
                    break;

                case 0x38:
                    builder.Append(off);
                    builder.Append("前冲，突然背后运球过了");
                    builder.Append(def);
                    builder.Append("，跳投得分！");
                    break;

                case 0x39:
                    builder.Append(off);
                    builder.Append("体前来回拉扯运球，突然急停跳投命中！");
                    break;

                case 0x3a:
                    builder.Append(off);
                    builder.Append("胯下运球转身，投篮得手 ！");
                    break;

                case 0x3b:
                    builder.Append(off);
                    builder.Append("脚踩“三分线”，在");
                    builder.Append(def + "眼前高高拔起出手，球空心入网！");
                    break;

                case 60:
                    builder.Append(off);
                    builder.Append("转身强打");
                    builder.Append(def);
                    builder.Append("，跳步投篮命中！");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffenseInsideDunk(string off, string def)
        {
            int num = rnd.Next(0, 0x13);
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
                    builder.Append("内线高高跃起，一个反手扣篮。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("内线大力扣篮得分。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("在" + def + "头顶一记重扣得分。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("强打内线得手。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("本次进攻中取得绝对内线优势，单手扣篮，");
                    builder.Append(def);
                    builder.Append("只能仰视。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("面对篮筐双手扣篮得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("挤进内线，勾手，球进！");
                    builder.Append(def);
                    builder.Append("的防守起不到半点作用。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("空中躲过");
                    builder.Append(def);
                    builder.Append("封盖，扣篮得分。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("跨下运球晃过防守，面对篮筐一次单手扣篮。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("的软弱的内线防守，来了一个大风车扣篮。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("顶到内线，假动作晃开");
                    builder.Append(def);
                    builder.Append("，一个优雅的挑篮得分。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("单手挟球起跳，大风车扣篮。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("卡住");
                    builder.Append(def);
                    builder.Append("脚步，一个转身，篮下打板入筐。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("护球转身，绕过");
                    builder.Append(def);
                    builder.Append("，双手灌篮，随着篮球入筐，篮架子也跟着晃了晃。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("防守，把篮球砸进篮筐。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("再现奥拉朱旺梦幻舞步，骗晕");
                    builder.Append(def);
                    builder.Append("，轻松扣篮。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("防守，旱地拔葱灌篮。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("起跳，空中躲过");
                    builder.Append(def);
                    builder.Append("盖帽，扭腰从篮板右侧勾手打板进球。");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffenseInsideDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 15);
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
                    builder.Append("的防守，传球内线，");
                    builder.Append(off);
                    builder.Append("爆扣得分！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("倚住");
                    builder.Append(def);
                    builder.Append("，接到");
                    builder.Append(pass);
                    builder.Append("的传球后，原地转身扣篮！");
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
                    builder.Append("就是一记灌篮！惊人！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("在空中接到");
                    builder.Append(pass);
                    builder.Append("的传球，避开");
                    builder.Append(def);
                    builder.Append("的拦截，换手扣篮得分！SHOW！");
                    break;

                case 7:
                    builder.Append(def);
                    builder.Append("被");
                    builder.Append(pass);
                    builder.Append("轻松绕过，将球传给内线");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("重扣得分！");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("将球抛向篮筐，");
                    builder.Append(off);
                    builder.Append("空中抓球一个大回环，漂亮的风车灌篮！经典！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("挤进内线，空中接");
                    builder.Append(pass);
                    builder.Append("传球，闪躲腾挪，从篮板另一侧将球勾进。");
                    break;

                case 10:
                    builder.Append(pass);
                    builder.Append("传球无人防守的内线，");
                    builder.Append(off);
                    builder.Append("双手持球起跳，转身背扣得手。");
                    break;

                case 11:
                    builder.Append(pass);
                    builder.Append("突破分球，");
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("防守，空中“飞翔”灌篮！");
                    break;

                case 12:
                    builder.Append(pass);
                    builder.Append("运球至内线吸引");
                    builder.Append(def);
                    builder.Append("防守，传球给");
                    builder.Append(off);
                    builder.Append("，后者接球大风车暴扣！");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("接球顺势一个跳步，摆脱");
                    builder.Append(def);
                    builder.Append("防守，单手扣篮！");
                    builder.Append(pass);
                    builder.Append("助攻！");
                    break;

                case 14:
                    builder.Append(pass);
                    builder.Append("背对");
                    builder.Append(def);
                    builder.Append("，突然一个右转身，摆脱防守，传球篮下，");
                    builder.Append(off);
                    builder.Append("接球后，双手扣篮表演！");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffenseInsideGoodDunk(string off, string def)
        {
            int num = rnd.Next(0, 11);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("带球突破，突然跳起360度转身挑篮，球进了！震撼！");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("长驱直入，双手灌篮得分。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("连续的胯下运球，使");
                    builder.Append(def);
                    builder.Append("放松了警惕，突然突破，扣篮！");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("运球左右变向，将");
                    builder.Append(def);
                    builder.Append("晃得失去重心，突入内线，双手重扣得分！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("虚晃一下，直接杀进内线，在");
                    builder.Append(def);
                    builder.Append("头顶强行灌篮。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("冲进内线，空中闪躲，摆脱防守，从篮板另一侧将球勾进。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("防守，空中“飞翔”大灌篮。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("突破了");
                    builder.Append(def);
                    builder.Append("的防守，起跳，身体在空中再次伸展，双手把篮球背扣进篮筐。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("已经失位的防守，空中抡圆一个圈将球狠狠地扣进篮筐。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("以一个具有表演性的扣篮侮辱了防守失位的");
                    builder.Append(def);
                    builder.Append("！");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("运球变向，将");
                    builder.Append(def);
                    builder.Append("晃倒在地，轻松单手扣篮！");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffenseInsideGoodDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 20);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(pass);
                    builder.Append("传球内线，");
                    builder.Append(off);
                    builder.Append("扣篮得分。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("助攻内线，");
                    builder.Append(off);
                    builder.Append("内线高高跃起，一个反手扣篮。");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("助攻内线，");
                    builder.Append(off);
                    builder.Append("内线大力扣篮得分。");
                    break;

                case 3:
                    builder.Append(pass);
                    builder.Append("突破分球，");
                    builder.Append(off);
                    builder.Append("内线接球爆扣。");
                    break;

                case 4:
                    builder.Append(pass);
                    builder.Append("佯装投篮，传球内线，");
                    builder.Append(off);
                    builder.Append("接球在");
                    builder.Append(def);
                    builder.Append("头顶暴扣！");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("内线卡住位置，接");
                    builder.Append(pass);
                    builder.Append("及时的传球，面对篮筐双手扣篮得分。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("接球顺势挤进内线，勾手，球进！");
                    builder.Append(pass);
                    builder.Append("助攻一次");
                    break;

                case 7:
                    builder.Append(pass);
                    builder.Append("跨下运球晃过防守，传球内线，");
                    builder.Append(off);
                    builder.Append("接球，单手扣篮。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("面对" + def + "的软弱的内线防守，牢牢卡住位置，接");
                    builder.Append(pass);
                    builder.Append("传球，来了一个大风车扣篮。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("顶到内线，接");
                    builder.Append(pass);
                    builder.Append("助攻，一个优雅的挑篮得分。");
                    break;

                case 10:
                    builder.Append(pass);
                    builder.Append("击地传球给内线，");
                    builder.Append(off);
                    builder.Append("单手挟球起跳，大风车扣篮。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("内线接到");
                    builder.Append(pass);
                    builder.Append("的传球，轻松将球扣入。");
                    break;

                case 12:
                    builder.Append(pass);
                    builder.Append("绕过");
                    builder.Append(def);
                    builder.Append("的防守，助攻内线，");
                    builder.Append(off);
                    builder.Append("一个漂亮的上篮得分。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("倚住");
                    builder.Append(def);
                    builder.Append("，接到");
                    builder.Append(pass);
                    builder.Append("的传球，在");
                    builder.Append(def);
                    builder.Append("的头顶打板得分。");
                    break;

                case 14:
                    builder.Append(pass);
                    builder.Append("趁");
                    builder.Append(def);
                    builder.Append("内线失去防守位置，助攻内线，");
                    builder.Append(off);
                    builder.Append("扣篮得分。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("底线接到");
                    builder.Append(pass);
                    builder.Append("的传球，翻身大勾手得分。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("的助攻，篮下强打得分。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("篮下跑出空挡接");
                    builder.Append(pass);
                    builder.Append("的传球，轻松扣篮。");
                    break;

                case 0x12:
                    builder.Append(pass);
                    builder.Append("把球吊进内线，");
                    builder.Append(off);
                    builder.Append("靠身体顶住");
                    builder.Append(def);
                    builder.Append("，灌篮得分。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("摆脱");
                    builder.Append(def);
                    builder.Append("防守，起跳空中接");
                    builder.Append(pass);
                    builder.Append("恰倒好处的传球，单手扣进蓝框！");
                    break;
            }
            return HighLightBlue(builder.ToString());
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
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffensePickDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 11);
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
                    builder.Append("做了个挡拆配合，" + off + "接球投篮命中。");
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
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffensePickGoodDunkWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 11);
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
            return HighLightBlue(builder.ToString());
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

        public static string GoodOffenseThreeWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 0x13);
            StringBuilder builder = new StringBuilder();
            builder.Append("[三分]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("零度角接到");
                    builder.Append(pass);
                    builder.Append("的传球，出手三分，");
                    builder.Append(def);
                    builder.Append("防守不及，命中！");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("突破未果，把球甩到外线，");
                    builder.Append(off);
                    builder.Append("接球超远距离出手三分命中。");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("传球到外线，");
                    builder.Append(off);
                    builder.Append("飚中三分。");
                    break;

                case 3:
                    builder.Append(pass);
                    builder.Append("受");
                    builder.Append(def);
                    builder.Append("严密防守，无奈把球传给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("三分果断出手，命中！");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球后，没有运球，直接出手，中了。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("空挡出手，进了。是");
                    builder.Append(pass);
                    builder.Append("传出的好球！");
                    break;

                case 6:
                    builder.Append(pass);
                    builder.Append("执行三分战术，传球到外线，");
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("防守下三分远投中的。");
                    break;

                case 7:
                    builder.Append(pass);
                    builder.Append("传球，");
                    builder.Append(off);
                    builder.Append("距离三分线还有一步的地方出手，命中！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("零度角命中一个三分球，是");
                    builder.Append(pass);
                    builder.Append("传出的好球。");
                    break;

                case 9:
                    builder.Append(pass);
                    builder.Append("突破后分球到外线，");
                    builder.Append(off);
                    builder.Append("接球轻松命中三分。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，突然外线跳投，");
                    builder.Append(def);
                    builder.Append("差点盖到皮球，球还是进了。");
                    break;

                case 11:
                    builder.Append(pass);
                    builder.Append("在外线连续胯下运球与");
                    builder.Append(def);
                    builder.Append("的防守周旋，突然助攻");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("出手三分命中！");
                    break;

                case 12:
                    builder.Append(pass);
                    builder.Append("佯装突破，突然传到外线，");
                    builder.Append(off);
                    builder.Append("接球果断出手三分，中的！");
                    break;

                case 13:
                    builder.Append(pass);
                    builder.Append("助攻");
                    builder.Append(off);
                    builder.Append("三分远投中的。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("的传球，稳稳的投中三分。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("晃开");
                    builder.Append(def);
                    builder.Append("的防守，接");
                    builder.Append(pass);
                    builder.Append("的传球三分远投中的。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("外线空档接");
                    builder.Append(pass);
                    builder.Append("传球投中三分。");
                    break;

                case 0x11:
                    builder.Append("接到");
                    builder.Append(pass);
                    builder.Append("传球，");
                    builder.Append(off);
                    builder.Append("外线投中三分。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("零度角接");
                    builder.Append(pass);
                    builder.Append("传球投进三分。");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string GoodOffInsideWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 15);
            StringBuilder builder = new StringBuilder();
            builder.Append("[内线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("内线卡位，接");
                    builder.Append(pass);
                    builder.Append("的吊球强打，零度角跳投得分。");
                    break;

                case 1:
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("防守，");
                    builder.Append(off);
                    builder.Append("内线强打得分。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("内线接球，做了个左转身的假动作，骗开");
                    builder.Append(def);
                    builder.Append("，右转身轻松投中。");
                    builder.Append(pass);
                    builder.Append("助攻一次。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("内线接球，45度角跳投得分。");
                    builder.Append(pass);
                    builder.Append("助攻一次。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("内线接到");
                    builder.Append(pass);
                    builder.Append("的传球，轻松投中一球。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("空切篮下，得");
                    builder.Append(pass);
                    builder.Append("妙传，跳投得分。");
                    break;

                case 6:
                    builder.Append(pass);
                    builder.Append("传球内线，");
                    builder.Append(off);
                    builder.Append("篮下勾手得分。");
                    break;

                case 7:
                    builder.Append(pass);
                    builder.Append("助攻，");
                    builder.Append(off);
                    builder.Append("，右转身轻松投中。好一个妙传！");
                    break;

                case 8:
                    builder.Append(pass);
                    builder.Append("吊球内线");
                    builder.Append(off);
                    builder.Append("甩开防守，篮下接球得分。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("篮下接到");
                    builder.Append(pass);
                    builder.Append("恰倒好处的传球，出手得分。");
                    break;

                case 10:
                    builder.Append(pass);
                    builder.Append("绕过");
                    builder.Append(def);
                    builder.Append("的防守，助攻");
                    builder.Append(off);
                    builder.Append("禁区投篮得分。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("倚住");
                    builder.Append(def);
                    builder.Append("，接到");
                    builder.Append(pass);
                    builder.Append("的传球，打板得分。");
                    break;

                case 12:
                    builder.Append(pass);
                    builder.Append("趁");
                    builder.Append(def);
                    builder.Append("内线失去防守位置，助攻");
                    builder.Append(off);
                    builder.Append("轻松得分。");
                    break;

                case 13:
                    builder.Append(pass);
                    builder.Append("做了个假动作，骗开");
                    builder.Append(def);
                    builder.Append("，助攻篮下的");
                    builder.Append(off);
                    builder.Append("得分。");
                    break;

                case 14:
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

        public static string GoodOffShot(string off, string def)
        {
            int num = rnd.Next(0, 30);
            StringBuilder builder = new StringBuilder();
            builder.Append("[外线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("中距离出手，投中一球。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("中投，拿下一球。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("跑动中跳投命中一球。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("左路跳投得手。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("罚球线附近避开");
                    builder.Append(def);
                    builder.Append("干扰，跳投得分。");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("急停跳投中的。");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("圈顶出手得分。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("右翼跳投得分。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("左翼跳投命中。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("突破，急停跳投得分。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("投篮得分。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("零度角晃过");
                    builder.Append(def);
                    builder.Append("跳投得分。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("带球，后撤步急停跳投中的。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("面对");
                    builder.Append(def);
                    builder.Append("的松散防守，直接跳投得分。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("一个假动作，跳起投篮，得分。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("佯装突破，突然投篮得分。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("跑动中出手得分。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("后仰投篮进了。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("在底线出手，把球投中。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("胯下运球，外线跳投命中。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("禁区外右侧大勾手把球投中。");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("罚球线附近转身后仰投篮命中。");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("脚踏3分线，看到");
                    builder.Append(def);
                    builder.Append("缩在内线防守，毫不犹豫出手投中一个远投。");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("虚晃了一下");
                    builder.Append(def);
                    builder.Append("，拉出空档中投得手。");
                    break;

                case 0x18:
                    builder.Append(off);
                    builder.Append("篮板左侧胯下运球，起跳打板得分。");
                    break;

                case 0x19:
                    builder.Append(off);
                    builder.Append("控球，乘");
                    builder.Append(def);
                    builder.Append("防守松懈，远抛得手。");
                    break;

                case 0x1a:
                    builder.Append(off);
                    builder.Append("胯下运球，体前变向，突破了");
                    builder.Append(def);
                    builder.Append("的防守，投篮命中。");
                    break;

                case 0x1b:
                    builder.Append(off);
                    builder.Append("持球侧对着篮筐，突然一个转身摆脱了");
                    builder.Append(def);
                    builder.Append("，正面打板将球送进篮筐。");
                    break;

                case 0x1c:
                    builder.Append(off);
                    builder.Append("强行突破，中投得手。");
                    break;

                case 0x1d:
                    builder.Append(off);
                    builder.Append("背对");
                    builder.Append(def);
                    builder.Append("，转身后仰投篮，球进。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffShotWithAssist(string off, string pass, string def)
        {
            int num = rnd.Next(0, 14);
            StringBuilder builder = new StringBuilder();
            builder.Append("[外线]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("中距离接");
                    builder.Append(pass);
                    builder.Append("传球，投中两分。");
                    break;

                case 1:
                    builder.Append(pass);
                    builder.Append("突破分球，");
                    builder.Append(off);
                    builder.Append("中投，拿下两分。");
                    break;

                case 2:
                    builder.Append(pass);
                    builder.Append("突破分球左路，");
                    builder.Append(off);
                    builder.Append("无人防守，跳投得手。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("罚球线附近接");
                    builder.Append(pass);
                    builder.Append("传球避开");
                    builder.Append(def);
                    builder.Append("干扰，跳投得分。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("空挡急停跳投中的。");
                    builder.Append(pass);
                    builder.Append("的助攻。");
                    break;

                case 5:
                    builder.Append(pass);
                    builder.Append("佯装突破，分球给外线的");
                    builder.Append(off);
                    builder.Append("，后者远投空心入网。");
                    break;

                case 6:
                    builder.Append(pass);
                    builder.Append("分球给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("中投打板入筐。");
                    break;

                case 7:
                    builder.Append(pass);
                    builder.Append("分球到外线的");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("见无人防守，果断出手，命中！");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，果断出手，空心入网。");
                    break;

                case 9:
                    builder.Append(pass);
                    builder.Append("突破后把球分给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("闪开");
                    builder.Append(def);
                    builder.Append("封盖，跳投得分。");
                    break;

                case 10:
                    builder.Append(pass);
                    builder.Append("把球传给");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("圈顶出手命中。");
                    break;

                case 11:
                    builder.Append(pass);
                    builder.Append("突破后助攻底线投手");
                    builder.Append(off);
                    builder.Append("，");
                    builder.Append(off);
                    builder.Append("稳稳命中两分。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("接");
                    builder.Append(pass);
                    builder.Append("传球，后撤步跳投稳稳命中。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("传");
                    builder.Append(pass);
                    builder.Append("后,");
                    builder.Append("在外线接");
                    builder.Append(pass);
                    builder.Append("及时的回传，命中两分。");
                    break;
            }
            return builder.ToString();
        }

        public static string GoodOffThree(string off, string def)
        {
            int num = rnd.Next(0, 10);
            StringBuilder builder = new StringBuilder();
            builder.Append("[快攻]");
            switch (num)
            {
                case 0:
                    builder.Append(off);
                    builder.Append("在外线连续胯下运球与");
                    builder.Append(def);
                    builder.Append("的防守周旋，突然出手，三分球进。");
                    break;

                case 1:
                    builder.Append(off);
                    builder.Append("被");
                    builder.Append(def);
                    builder.Append("的防守压迫到外线，");
                    builder.Append(off);
                    builder.Append("无奈出手，球打板入网，三分球进。");
                    break;

                case 2:
                    builder.Append(off);
                    builder.Append("三分远投命中。");
                    break;

                case 3:
                    builder.Append(off);
                    builder.Append("佯装突破，突然外线直接出手，三分中的，");
                    builder.Append(def);
                    builder.Append("只能望球兴叹。");
                    break;

                case 4:
                    builder.Append(off);
                    builder.Append("右翼命中！THREE！！！");
                    break;

                case 5:
                    builder.Append(off);
                    builder.Append("命中一个三分球！THREE！");
                    break;

                case 6:
                    builder.Append(off);
                    builder.Append("跳投三分球还以颜色。");
                    break;

                case 7:
                    builder.Append(off);
                    builder.Append("三分球出手命中。");
                    break;

                case 8:
                    builder.Append(off);
                    builder.Append("三分长射，进了。");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("圈顶发炮，三分，进了。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("放三分，球进了。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("外线右翼飙中三分。THREE！");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("外线左翼飙中三分。THREE！");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("外线打中三分。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("零度角命中一个三分。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("无视");
                    builder.Append(def);
                    builder.Append("外线防守，拿球便投，三分。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("远投三分得手。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("三分球出手命中。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("三分远投中的。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("外线投中三分。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("防守下三分远投中的。");
                    break;

                case 0x15:
                    builder.Append("接到传球，");
                    builder.Append(off);
                    builder.Append("外线投中三分。");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("外线右翼飙中三分。");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("在");
                    builder.Append(def);
                    builder.Append("防守下三分远投中的。");
                    break;

                case 0x18:
                    builder.Append(off);
                    builder.Append("弧顶投中三分。");
                    break;

                case 0x19:
                    builder.Append(off);
                    builder.Append("三分远投还以颜色。");
                    break;

                case 0x1a:
                    builder.Append(off);
                    builder.Append("零度角出手，");
                    builder.Append(def);
                    builder.Append("防守不及，球投进。");
                    break;
            }
            return HighLightBlue(builder.ToString());
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
            int num = rnd.Next(0, 30);
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

                case 0x12:
                    builder.Append(off);
                    builder.Append("带球撞人，进攻犯规。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("脚踢球违例。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("带球撞人。");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("肘击");
                    builder.Append(def);
                    builder.Append("进攻犯规。");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("推人，进攻犯规。");
                    break;

                case 0x17:
                    builder.Append(off);
                    builder.Append("进攻推人犯规。");
                    break;

                case 0x18:
                    builder.Append(off);
                    builder.Append("运球时推开");
                    builder.Append(def);
                    builder.Append("，进攻犯规。");
                    break;

                case 0x19:
                    builder.Append(off);
                    builder.Append("转身过人时，手部有个扒人动作，进攻犯规。");
                    break;

                case 0x1a:
                    builder.Append(off);
                    builder.Append("强打篮下时，肩部撞人犯规。");
                    break;

                case 0x1b:
                    builder.Append(off);
                    builder.Append("三步上篮，撞倒");
                    builder.Append(def);
                    builder.Append("，进攻犯规。");
                    break;

                case 0x1c:
                    builder.Append(off);
                    builder.Append("一只手运球，另一只手推搡");
                    builder.Append(def);
                    builder.Append("，犯规。");
                    break;

                case 0x1d:
                    builder.Append(off);
                    builder.Append("起跳勾手时，左手压人。");
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
                    builder.Append("伸手拿下队友拨过来的篮球。");
                    break;

                case 0x11:
                    builder.Append(offReb);
                    builder.Append("绕前卡位，提前起跳，抢到进攻篮板。");
                    break;

                case 0x12:
                    builder.Append(offReb);
                    builder.Append("冲到篮下，高高跃起抓下篮板。");
                    break;

                case 0x13:
                    builder.Append(offReb);
                    builder.Append("从对方手中抢到篮板。");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string Steal(string off, string def)
        {
            int num = rnd.Next(0, 0x10);
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
                    builder.Append("换手运球，似乎并不习惯，果然被");
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

                case 10:
                    builder.Append(off);
                    builder.Append("运球，被");
                    builder.Append(def);
                    builder.Append("偷了！");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("运球，被断掉了！");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("传球，被");
                    builder.Append(def);
                    builder.Append("看穿球路，直接断下皮球。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("传球，被");
                    builder.Append(def);
                    builder.Append("断了下来。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("传球，被");
                    builder.Append(def);
                    builder.Append("卡住路线，断了下来。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("传球，被");
                    builder.Append(def);
                    builder.Append("断了！");
                    break;
            }
            return HighLightBlue(builder.ToString());
        }

        public static string TO(string off, string def)
        {
            int num = rnd.Next(0, 0x17);
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
                    builder.Append("传球出界！");
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
                    builder.Append("掷界外球违例！");
                    break;

                case 9:
                    builder.Append(off);
                    builder.Append("传球失误，出了边线。");
                    break;

                case 10:
                    builder.Append(off);
                    builder.Append("传球回线。");
                    break;

                case 11:
                    builder.Append(off);
                    builder.Append("运球回线。");
                    break;

                case 12:
                    builder.Append(off);
                    builder.Append("脚下滑了一下，造成走步。");
                    break;

                case 13:
                    builder.Append(off);
                    builder.Append("过人突破时走步。");
                    break;

                case 14:
                    builder.Append(off);
                    builder.Append("竟然二运了。");
                    break;

                case 15:
                    builder.Append(off);
                    builder.Append("发球违例。");
                    break;

                case 0x10:
                    builder.Append(off);
                    builder.Append("进攻3秒，可惜。");
                    break;

                case 0x11:
                    builder.Append(off);
                    builder.Append("进攻3秒。");
                    break;

                case 0x12:
                    builder.Append(off);
                    builder.Append("接球不稳，球从手中滑出边线。");
                    break;

                case 0x13:
                    builder.Append(off);
                    builder.Append("将球传出了底线。");
                    break;

                case 20:
                    builder.Append(off);
                    builder.Append("脚踩到了底线之外。");
                    break;

                case 0x15:
                    builder.Append(off);
                    builder.Append("强打时被吹走步。");
                    break;

                case 0x16:
                    builder.Append(off);
                    builder.Append("传球出界。");
                    break;
            }
            return HighLightRed(builder.ToString());
        }
    }
}

