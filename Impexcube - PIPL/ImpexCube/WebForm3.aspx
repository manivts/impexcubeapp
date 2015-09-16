﻿<?xml version="1.0" encoding="UTF-8"?><!--
  For more information on how to configure your ASP.NET application, please visit
  http://go.microsoft.com/fwlink/?LinkId=169433
  --><configuration>
  <system.web>
<httpRuntime maxRequestLength="1048576" />
    <pages>
      <controls>
        <add tagPrefix="asp" namespace="System.Web.UI.DataVisualization.Charting" assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
      </controls>
    </pages>
    <customErrors mode="Off"></customErrors>
    <sessionState timeout="2000" />
    <httpHandlers>
      <add path="Reserved.ReportViewerWebControl.axd" verb="*" type="Microsoft.Reporting.WebForms.HttpHandler, Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" validate="false" />
      <add path="ChartImg.axd" verb="GET,HEAD,POST" type="System.Web.UI.DataVisualization.Charting.ChartHttpHandler, System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" validate="false" />
    <add verb="GET" path="CrystalImageHandler.aspx" type="CrystalDecisions.Web.CrystalImageHandler, CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" /></httpHandlers>
    <compilation targetFramework="4.0">
      <assemblies>
        <add assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" />
        <add assembly="Microsoft.ReportViewer.Common, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" />
        <add assembly="Microsoft.Build.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" />
        <add assembly="System.Management, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" />
        <add assembly="System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
        <add assembly="System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" />
        <add assembly="System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089" />
        <add assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
      </assemblies>
      <buildProviders>
        <add extension=".rdlc" type="Microsoft.Reporting.RdlBuildProvider, Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" />
      </buildProviders>
    </compilation>
    <identity impersonate="true" />
  </system.web>
  <connectionStrings>
    <!--<add name="Constr" connectionString="Server=ImpexCubedb.db.10295544.hostedresource.com;Initial Catalog=ImpexCubedb;Persist Security Info=True;User ID=ImpexCubedb;Password=Corp#2012" providerName="System.Data.SqlClient"/>-->
       <add name="Constr" connectionString="Data Source=SERVER\SQLEXPRESS;Initial Catalog=ImpexCube;Persist Security Info=True;User ID=sa;Password=Translink_123" providerName="System.Data.SqlClient" />
    <!--<add name="Constr" connectionString="Data Source=63.143.40.147,1232;Initial Catalog=ImpexCube;Persist Security Info=True;User ID=sa;Password=4UTXBBrBEUV8siQ^Je" providerName="System.Data.SqlClient"/>--><add name="VTSConstr" connectionString="Data Source=server\sqlexpress;Initial Catalog=Integration;Persist Security Info=True;User ID=sa;Password=Translink_123" providerName="System.Data.SqlClient"/>
      </connectionStrings>
  <appSettings>
    <add key="ChartImageHandler" value="storage=file;timeout=20;dir=c:\TempImageFiles\;" />
 

    <add key="ConnectionDashboard" value="server=SERVER\SQLEXPRESS;uid=sa;pwd=Translink_123;database=ImpexCube;" />

   <add key="ConnectionVisual" value="director;uid=root;pwd=;database=VisualIMPEX-VI0000000827;Max Pool Size=50000;"/>
    <!-- <add key="ConnectionDashboard" value="server=63.143.40.147,1232;uid=sa;pwd=4UTXBBrBEUV8siQ^Je;database=ImpexCube;"/>-->
   </appSettings>
    
  <system.webServer>
    <validation validateIntegratedModeConfiguration="false" />
    <handlers>
      <remove name="ChartImageHandler" />
      <add name="ReportViewerWebControlHandler" preCondition="integratedMode" verb="*" path="Reserved.ReportViewerWebControl.axd" type="Microsoft.Reporting.WebForms.HttpHandler, Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" />
      <add name="ChartImageHandler" preCondition="integratedMode" verb="GET,HEAD,POST" path="ChartImg.axd" type="System.Web.UI.DataVisualization.Charting.ChartHttpHandler, System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
    <add name="CrystalImageHandler.aspx_GET" verb="GET" path="CrystalImageHandler.aspx" type="CrystalDecisions.Web.CrystalImageHandler, CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" preCondition="integratedMode" /></handlers>
        <defaultDocument>
            <files>
                <remove value="pipl.aspx" />
                <add value="frmLogin.aspx" />
            </files>
        </defaultDocument>
  </system.webServer>
</configuration>