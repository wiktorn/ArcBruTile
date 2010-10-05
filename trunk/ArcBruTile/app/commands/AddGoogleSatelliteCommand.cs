﻿using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BrutileArcGIS.Properties;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
namespace BruTileArcGIS
{
    /// <summary>
    /// ArcGIS Command to show a BruTile.
    /// </summary>
    [Guid("9918D3FB-6905-42E8-8709-E894E7F0482E")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("AddGoogleSatelliteCommand")]
    public sealed class AddGoogleSatelliteCommand : BaseCommand
    {
        #region private members
        private IMap map;
        private IApplication application;
        #endregion

        #region constructors
        /// <summary>
        /// Initialises a new BruTileCommand.
        /// </summary>
        public AddGoogleSatelliteCommand()
        {
            base.m_category = "BruTile";
            base.m_caption = "&Google";
            base.m_message = "Add Google Satellite Layer";
            base.m_toolTip = base.m_message;
            base.m_name = "AddGoogleLayer";
            base.m_bitmap = Resources.google;
        }
        #endregion

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="BerekenenDHMCommand"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public override bool Enabled
        {
            get
            {
                return true;
            }
        }




        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            try
            {
                Configuration config = ConfigurationHelper.GetConfig();
                IMxDocument mxdoc = (IMxDocument)application.Document;
                map = mxdoc.FocusMap;
                BruTileLayer brutileLayer = new BruTileLayer(application, EnumBruTileLayer.GoogleSatellite);
                brutileLayer.Name = "Google Satellite";

                brutileLayer.Visible = true;
                map.AddLayer((ILayer)brutileLayer);
                //map.MoveLayer((ILayer)brutileLayer, map.LayerCount);
                Util.SetBruTilePropertyPage(application, brutileLayer);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ", " + ex.StackTrace);
            }
        }

        #endregion

        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }
        #endregion

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion

    }
}