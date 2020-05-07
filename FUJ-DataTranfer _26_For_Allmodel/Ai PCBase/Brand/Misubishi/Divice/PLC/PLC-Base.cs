﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBase_Interface.Brand.Misubishi.Divice.PLC
{
    public class PLC_Base
    {
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("PLC"), Browsable(true), Description("ID")]
        public string Id
        {
            get;
            set;
        } = "1";
        /// <summary>
        /// Part Index
        /// </summary>
        [Category("PLC"), Browsable(true), Description("Description")]
        public string Description
        {
            get;
            set;
        } = "CPU";
        /// <summary>
        /// Part Status
        /// </summary>
        [Category("PLC"), Browsable(true), Description("Part Status")]
        public Error Status
        {
            get;
            set;
        } = Error.Normal;
        /// <summary>
        /// Part Status
        /// </summary>
        [Category("PLC"), Browsable(true), Description("Error")]
        public bool Errors
        {
            get
            {
                var result = this.Status != Error.Normal ? true : false;
                return result;
            }

            set
            {
                Errors = value;

            }
        }
        /// <summary>
        /// Part Status
        /// </summary>
        [Category("PLC"), Browsable(true), Description("Name")]
        public string Name
        {
            get;
            set;
        } = "PLC Control Sequence";
        /// <summary>
        /// Part Status
        /// </summary>
        [Category("PLC"), Browsable(true), Description("Model")]
        public string Model
        {
            get;
            set;
        } = "Q02UDPU";
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PLC_Base() { }

        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="name"></param>
        public PLC_Base(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="name"></param>
        public PLC_Base(string name, string id)
        {
            Id = id;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Initialization_Process()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Initialization()
        {
            Initialization_Process();
        }
    }
}

namespace PCBase_Interface
{
    public enum Error
    {
        Normal,
        PowerOff,
        CommuFail,

    }
}