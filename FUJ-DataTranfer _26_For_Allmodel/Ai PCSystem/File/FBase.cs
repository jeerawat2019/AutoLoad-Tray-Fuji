using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Ai_PCSystem.File
{
    public class FBase 
    {
        [XmlIgnore]
        [Category("Indexing"), Browsable(true), Description("Is end station index")]
        public string ApplicationName
        {
            get;
            set;
        } = string.Empty;
        [XmlIgnore]
        [Category("Indexing"), Browsable(true), Description("Is end station index")]
        public string PathMainApp
        {
            get;
            set;
        } = string.Empty;
        [XmlIgnore]
        [Category("Indexing"), Browsable(true), Description("Is end station index")]
        public virtual List<string> AllFolderApp
        {
            get;
            internal set;
        } = new List<string>();
        [XmlIgnore]
        [Category("Indexing"), Browsable(true), Description("Is end station index")]
        public virtual string MainFolderAppName
        {
            get;
            set;
        } = string.Empty;
        [XmlIgnore]
        [Category("Indexing"), Browsable(true), Description("Is end station index")]
        public virtual string SubFolderAppName
        {
            get;
            set;
        } = string.Empty;
        [XmlIgnore]
        [Category("Indexing"), Browsable(true), Description("Is end station index")]
        public virtual string FileName
        {
            get;
            set;
        } = string.Empty;
    }
}
