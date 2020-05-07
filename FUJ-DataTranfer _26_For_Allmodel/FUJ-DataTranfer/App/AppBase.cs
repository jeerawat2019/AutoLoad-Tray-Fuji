using PCBase_Interface.Brand.Misubishi.Divice.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUJ_DataTranfer.App
{
    public class AppBase : INotifyPropertyChanged, IDisposable
    {
        public PLC_Commu PLC_Commu { get; set; } = new PLC_Commu();
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Q02UCDP_MemConfig_Tray { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Q02UCDP_MemConfig_Product { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public iServer IServer { get; set; } = new iServer();
        /// <summary>
        /// 
        /// </summary>
        public bool BypassServer { get; set; }

        public bool BypassDeleteFileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string InputResultPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OutputResultPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public Dictionary<string, string> Q02UCDP_MemConfig_Tray2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AppBase()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sName"></param>
        public AppBase(string sName)
        {
            InitializeIDReferences();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void StationInitialized()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void InitializeIDReferences()
        {
            this.StationInitialized();
        }
        private string[] MeasResult;
        /// <summary>
        /// 
        /// </summary>
        public string[] GetDataMeas
        {
            get { return MeasResult; }

            set
            {
                // Only update value if it changed
                if (value != MeasResult) {
                    MeasResult = value;
                    // Call NotifyPropertyChanged when the property is updated
                    NotifyPropertyChanged("GetDataMeas");
                }
            }
        }
        private State_Machine.Station.PickAndPlace.StateMC mStateMC = State_Machine.Station.PickAndPlace.StateMC.StationInitial;
        /// <summary>
        /// 
        /// </summary>
        public State_Machine.Station.PickAndPlace.StateMC PStateMC
        {
            get { return mStateMC; }

            set
            {
                // Only update value if it changed
                if (value != mStateMC) {
                    mStateMC = value;
                    // Call NotifyPropertyChanged when the property is updated
                    NotifyPropertyChanged("StateMC");
                }
            }
        }
        //private string mPartModel = null;
        //public string PartModel 
        //{
        //    get { return mPartModel; }
        //    set
        //    {
        //        // Only update value if it changed
        //        if (value != mPartModel)
        //        {
        //            mPartModel = value;
        //            // Call NotifyPropertyChanged when the property is updated
        //            NotifyPropertyChanged("PartModel");
        //        }
        //    }
        //}

        private List<Ai_Product.Product.PartResult> mGetPartListCsv = new List<Ai_Product.Product.PartResult>();
        /// <summary>
        /// 
        /// </summary>
        public List<Ai_Product.Product.PartResult> GetPartListCsv
        {
            get { return mGetPartListCsv; }
            set
            {
                // Only update value if it changed
                if (value != mGetPartListCsv) {
                    mGetPartListCsv = value;
                    // Call NotifyPropertyChanged when the property is updated
                    NotifyPropertyChanged("GetPartList");
                }
            }
    }
        // Declare the PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged will raise the PropertyChanged event passing the
        // source property that is being updated.
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
