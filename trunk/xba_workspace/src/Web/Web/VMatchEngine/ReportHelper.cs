namespace Web.VMatchEngine
{
    using System;
    using System.Text;

    internal class ReportHelper
    {
        public static void BuildPlayerrReport(StringBuilder sb, Player player, int arrangeIndex)
        {
            sb.Append("\t<Player PlayerID=\"");
            sb.Append(player.longPlayerID);
            sb.Append("\">");
            sb.Append("\t\t<ArrangeID>");
            sb.Append(arrangeIndex);
            sb.Append("</ArrangeID>");
            sb.Append("\t\t<Name>");
            sb.Append(player.strName);
            sb.Append("</Name>");
            sb.Append("\t\t<Number>");
            sb.Append(player.intNumber);
            sb.Append("</Number>");
            sb.Append("\t\t<Age>");
            sb.Append(player.intAge);
            sb.Append("</Age>");
            sb.Append("\t\t<Pos>");
            sb.Append(player.intPos);
            sb.Append("</Pos>");
            sb.Append("\t\t<Height>");
            sb.Append(player.intHeight);
            sb.Append("</Height>");
            sb.Append("\t\t<Weight>");
            sb.Append(player.intWeight);
            sb.Append("</Weight>");
            sb.Append("\t\t<Ability>");
            if (player.intCategory == 3)
            {
                sb.Append((float) 99.9f);
            }
            else
            {
                sb.Append((float) (((float) player.intAbility) / 10f));
            }
            sb.Append("</Ability>");
            sb.Append("\t\t<Power>");
            sb.Append((float) player.intPower);
            sb.Append("</Power>");
            sb.Append("\t</Player>");
        }
    }
}

