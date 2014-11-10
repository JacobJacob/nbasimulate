namespace Web.MyControls
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:EditBox runat=server></{0}:EditBox>")]
    public class EditBox : WebControl, IPostBackDataHandler
    {
        public event EventHandler TextChanged;

        public virtual bool LoadPostData(string postDataKey, NameValueCollection values)
        {
            string text = this.Text;
            string str2 = values[postDataKey];
            if (!text.Equals(str2))
            {
                this.Text = str2;
                return true;
            }
            return false;
        }

        protected virtual void OnTextChanged(EventArgs e)
        {
            if (this.TextChanged != null)
            {
                this.TextChanged(this, e);
            }
        }

        public virtual void RaisePostDataChangedEvent()
        {
            this.OnTextChanged(EventArgs.Empty);
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(string.Concat(new object[] { "<STYLE TYPE=\"text/css\">TABLE#tblCoolbar { BORDER-RIGHT: buttonshadow 1px solid; PADDING-RIGHT: 1px; BORDER-TOP: buttonhighlight 1px solid; PADDING-LEFT: 1px; PADDING-BOTTOM: 1px; BORDER-LEFT: buttonhighlight 1px solid; COLOR: menutext; PADDING-TOP: 1px; BORDER-BOTTOM: buttonshadow 1px solid; BACKGROUND-COLOR: buttonface }\t.cbtn { BORDER-RIGHT: buttonface 1px solid; BORDER-TOP: buttonface 1px solid; BORDER-LEFT: buttonface 1px solid; BORDER-BOTTOM: buttonface 1px solid; HEIGHT: 18px }\t.txtbtn { FONT-SIZE: 70%; COLOR: menutext; FONT-FAMILY: tahoma }</STYLE><script LANGUAGE=\"jscript\">function document.onreadystatechange(){try{idContent.document.designMode=\"On\";}catch(e){}}function GetResult(){\n\tvar strTemp;\tif(isHTMLMode){strTemp=idContent.document.body.innerText;\n\tstrTemp=strTemp.replace(/\"/gi,'');\n\tdocument.all.", this.UniqueID, ".value=strTemp;}\n\telse{strTemp=idContent.document.body.innerHTML;\n\tstrTemp=strTemp.replace(/\"/gi,'');\n\tdocument.all.", this.UniqueID, ".value=strTemp;}\n}\nfunction button_over(eButton){\teButton.style.backgroundColor = \"#B5BDD6\";\teButton.style.borderColor = \"darkblue darkblue darkblue darkblue\";}function button_out(eButton){\teButton.style.backgroundColor = \"threedface\";\teButton.style.borderColor = \"threedface\";}function button_down(eButton){\teButton.style.backgroundColor = \"#8494B5\";\teButton.style.borderColor = \"darkblue darkblue darkblue darkblue\";}function button_up(eButton){\teButton.style.backgroundColor = \"#B5BDD6\";\teButton.style.borderColor = \"darkblue darkblue darkblue darkblue\";\teButton = null; }var isHTMLMode=false;var bShow = false;function cmdExec(cmd,opt){\tif (isHTMLMode){alert(\"您现在是HTML编辑状态，无法使用此功能\");return;}\tidContent.focus();\tidContent.document.execCommand(cmd,bShow,opt);\tbShow=false;}function Image(arr)\n{\n\tidContent.focus();\n\tvar range=idContent.document.selection.createRange();\tvar ss;\n\tss=arr.split(\"*\");\n\ta=ss[0];\n\tb=ss[1];\n\tc=ss[2];\n\td=ss[3];\n\te=ss[4];\n\tf=ss[5];\n\tg=ss[6];\n\th=ss[7];\n\ti=ss[8];\n\tvar str1;\n\tarrA=a.split(\".\")\n\tif(arrA[1]==\"png\"&&(d<5||e<5||d==\"\"||e==\"\"))\n\t{\n\t\tstr1=\"<a href='a' title='点击观看图片' target='_blank'>点击观看图片</a>\";\n\t}\n\telse\n\t{\n\t\tstr1=\"<img src='\"+a+\"' \";\n\t\tif(d.valueOf()!='')str1=str1+\"width='\"+d+\"'\";\n\t\tif(e.valueOf()!='')str1=str1+\"height='\"+e+\"' \";\n\t\tif(b.valueOf()!='')str1=str1+\"alt='\"+b+\"' \";\n\t\tstr1=str1+\" border='\"+i+\"' align='\"+h+\"' vspace='\"+f+\"' hspace='\"+g+\"'  style='\"+c+\"'\";\n\t\tstr1=str1+\" onclick='javascript:window.open(this.src);' style='CURSOR: pointer' onmousewheel='BigImg(this);' onload='LoadImg(this);'>\";\n\t}\n\trange.pasteHTML(str1);\n}\nfunction setMode(bMode){\tvar sTmp;\tisHTMLMode = bMode;\tif (isHTMLMode){sTmp=idContent.document.body.innerHTML;idContent.document.body.innerText=sTmp;} \telse {sTmp=idContent.document.body.innerText;idContent.document.body.innerHTML=sTmp;};}function foreColor(){\tif (isHTMLMode){alert(\"您现在是HTML编辑状态，无法使用此功能\");return;}\tvar arr = showModalDialog(\"MyControls/HTMLEdit/selcolor.htm\",\"\",\"font-family:Verdana; font-size:10; dialogWidth:400px; dialogHeight:360px;status:0;\" );\tif (arr != null) cmdExec(\"ForeColor\",arr);\t}function createLink(){\tcmdExec(\"CreateLink\");}function insertImage(){\tif (isHTMLMode){alert(\"您现在是HTML编辑状态，无法使用此功能\");return;}\tvar arr = showModalDialog(\"MyControls/HTMLEdit/Image.htm\",\"\",\"dialogWidth:500px; dialogHeight:220px;status:0;\" );\tif(arr!=null) Image(arr);\telse idContent.focus();}function insertRuler(){\tcmdExec(\"InsertHorizontalRule\",\"\");}function insertFace(numI){\tvar strI=new String(numI);\tcmdExec(\"InsertImage\",\"MyControls/HTMLEdit/Face/\"+strI+\".gif\")}</script><table id=\"tblCoolbar\" width=\"500\" cellpadding=\"0\" cellspacing=\"0\">\t<tr valign=\"center\">\t\t<td width=\"23\"><div class=\"cbtn\" onClick=\"cmdExec('bold')\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t\t<img width=\"23\" height=\"22\" hspace=\"1\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/Bold.gif\" alt=\"粗体\">\t\t\t</div>\t\t</td>\t\t<td width=\"23\"><div class=\"cbtn\" onClick=\"cmdExec('italic')\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t\t<img width=\"23\" height=\"22\" hspace=\"1\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/Italic.gif\" alt=\"斜体\">\t\t\t</div>\t\t</td>\t\t<td width=\"23\"><div class=\"cbtn\" onClick=\"cmdExec('underline')\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t\t<img width=\"23\" height=\"22\" hspace=\"1\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/Under.gif\" alt=\"下划线\">\t\t\t</div>\t\t</td>\t\t<td width=\"23\"><div class=\"cbtn\" onClick=\"foreColor()\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t\t<img width=\"23\" height=\"22\" hspace=\"2\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/fgcolor.gif\" alt=\"颜色\">\t\t\t</div>\t\t</td>\t\t<td width=\"23\"><div class=\"cbtn\" onClick=\"cmdExec('createLink')\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t\t<img width=\"23\" height=\"22\" hspace=\"2\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/Link.gif\" alt=\"超链接\">\t\t\t</div>\t\t</td>\t\t<td width=\"23\"><div class=\"cbtn\" onClick=\"insertImage()\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t\t<img width=\"23\" height=\"22\" hspace=\"2\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/Image.gif\" alt=\"图片\">\t\t\t</div>\t\t</td>\t\t<td width=\"23\"><div class=\"cbtn\" onClick=\"insertRuler()\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t\t<img width=\"23\" height=\"22\" hspace=\"2\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/HR.gif\" alt=\"水平线\">\t\t\t</div>\t\t</td>\t\t<td width=\"170\" align=\"center\">\t\t\t<select onchange=\"cmdExec('fontname',this[this.selectedIndex].value);\">\t\t\t\t<option selected>字体</option>\t\t\t\t<option value=\"宋体\">宋体</option>\t\t\t\t<option value=\"黑体\">黑体</option>\t\t\t\t<option value=\"楷体_GB2312\">楷体</option>\t\t\t\t<option value=\"仿宋_GB2312\">仿宋</option>\t\t\t\t<option value=\"隶书\">隶书</option>\t\t\t\t<option value=\"幼圆\">幼圆</option>\t\t\t\t<option value=\"Arial\">Arial</option>\t\t\t\t<option value=\"Arial Black\">Arial Black</option>\t\t\t\t<option value=\"Arial Narrow\">Arial Narrow</option>\t\t\t\t<option value=\"Comic Sans MS\">Comic Sans MS</option>\t\t\t\t<option value=\"Courier New\">Courier New</option>\t\t\t\t<option value=\"System\">System</option>\t\t\t\t<option value=\"Tahoma\">Tahoma</option>\t\t\t\t<option value=\"Times New Roman\">Times New Roman</option>\t\t\t\t<option value=\"Verdana\">Verdana</option>\t\t\t\t<option value=\"Wingdings\">Wingdings</option>\t\t\t</select>\t\t</td>\t\t<td width=\"80\" align=\"center\">\t\t\t<select onchange=\"cmdExec('fontsize',this[this.selectedIndex].value);\">\t\t\t\t<option selected>字号</option>\t\t\t\t<option value=\"7\">一号</option>\t\t\t\t<option value=\"6\">二号</option>\t\t\t\t<option value=\"5\">三号</option>\t\t\t\t<option value=\"4\">四号</option>\t\t\t\t<option value=\"3\">五号</option>\t\t\t\t<option value=\"2\">六号</option>\t\t\t\t<option value=\"1\">七号</option>\t\t\t</select>\t\t</td>\t\t<td width=\"79\" align=\"center\"><input type=\"checkbox\" id=\"cbSetHtml\" onclick=\"setMode(this.checked)\"><font size=1 style=\"font-size: 8pt\">Html</font>\t\t</td>\t</tr><tr>\t<td width=\"23\"><div class=\"cbtn\" onClick=\"cmdExec('justifyright')\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t<img width=\"23\" height=\"22\" hspace=\"1\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/Right.gif\" alt=\"居右\">\t\t</div>\t</td>\t<td width=\"23\"><div class=\"cbtn\" onClick=\"cmdExec('justifyleft')\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t<img width=\"23\" height=\"22\" hspace=\"1\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/Left.gif\" alt=\"居左\">\t\t</div>\t</td>\t<td width=\"23\"><div class=\"cbtn\" onClick=\"cmdExec('justifycenter')\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t<img width=\"23\" height=\"22\" hspace=\"1\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/Center.gif\" alt=\"居中\">\t\t</div>\t</td>\t<td width=\"23\"><div class=\"cbtn\" onClick=\"cmdExec('RemoveFormat')\" onmouseover=\"button_over(this);\" onmouseout=\"button_out(this);\" onmousedown=\"button_down(this);\" onmouseup=\"button_up(this);\">\t\t\t<img width=\"23\" height=\"22\" hspace=\"2\" vspace=\"1\" align=\"absMiddle\" src=\"MyControls/HTMLEdit/removeformat.gif\" alt=\"取消格式\">\t\t</div>\t</td>\t<td width=\"408\" colspan=\"6\" align='right'>XBA篮球经理在线专用文本编辑器</td></tr><tr><td><input id=\"tbEditTextId\" name=\"", this.UniqueID, "\" type=\"hidden\" value=\"", this.Text, "\"></td></tr><tr><td height=\"", this.Height, "\" colspan=\"10\"><iframe id=\"idContent\" onBlur=\"GetResult()\" name=\"idContent\" marginheight=\"2\" marginwidth=\"2\" style=\"width:100%;height:100%;border-right: 0;border-left: 0;border-top: 0;border-bottom: 0;frameBorder: 0;\" src='EditFrame.htm'></IFRAME></table><script languate=\"jscript\">function GetBeginHtml(){\ttry\t{\t\tidContent.document.body.innerHTML=document.all.", this.UniqueID, ".value;\t}\tcatch(e)\t{\t}\tif(tag)return;\telse\t{\t\tsetTimeout(\"GetBeginHtml()\",1000);\t}}var begin=GetBeginHtml()</script>" }));
        }

        [DefaultValue(""), Bindable(true), Category("Appearance")]
        public string Text
        {
            get
            {
                return this.ViewState["Text"].ToString();
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }
    }
}

