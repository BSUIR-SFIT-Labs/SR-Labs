function ExtractText()
{
  TestedApps.Info_Hider.Run();
  
  let resultText = "Hello, world!";
  
  Aliases.InfoHider.MainWindow.CentralWidget.TabWidget.ClickTab("Get information");
  Aliases.InfoHider.MainWindow.CentralWidget.TabWidget
    .TabWidgetArea.GetInfoTab.ContainerWithDataSection.BtnUploadImageWithData.ClickButton();
  Aliases.InfoHider.OpenFileDialog.DUIViewWndClassName.Explorer_Pane
    .CtrlNotifySink.ShellView.Items_View.ResultImage_png.Name.DblClick(32, 0);
  Aliases.InfoHider.MainWindow.CentralWidget.TabWidget.TabWidgetArea.GetInfoTab
    .BtnExtractData.ClickButton();
  aqObject.CheckProperty(Aliases.InfoHider.MainWindow.CentralWidget.TabWidget.TabWidgetArea
    .GetInfoTab.ExtractedDataSection.ExtractedData, "QtText", cmpEqual, resultText);
  
  Aliases.InfoHider.MainWindow.Close();
}