
dotnet test --logger "xunit;LogFileName=UnitTestsResult.xml" --results-directory ./UnitTestsReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=UnitTestsReports\Coverage\ /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit.*]*

dotnet .\reportgenerator\4.2.9\tools\netcoreapp2.1\ReportGenerator.dll "-reports:./UnitTestsReports/Coverage/coverage.cobertura.xml" "-targetdir:./UnitTestsReports/Coverage"

start UnitTestsReports\Coverage\index.htm

pause