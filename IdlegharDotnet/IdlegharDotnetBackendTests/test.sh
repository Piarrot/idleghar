export CollectCoverage=true
export CoverletOutputFormat=cobertura,json
export CoverletOutput=./coverage/
export CoverletExcludeByFile=**/Program.cs
dotnet test
reportgenerator -reports:./coverage/coverage.cobertura.xml -targetdir:./coverage/reports
firefox ./coverage/reports/index.html