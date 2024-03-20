
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace ApiAntDatosvehiculo.Services
{
    public class CDescargaAnt
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;



        public CDescargaAnt(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public AntValoresPLacaDTO CConsultaDatosAutoAnt(PlacaDTO cedulaPlacaDTO)
        {


            ChromeOptions chromeOptions = new ChromeOptions();






            chromeOptions.AddArguments("--kiosk-printing");

            chromeOptions.AddUserProfilePreference("print.always_print_silent", true);

            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("printing.default_destination_selection_rules", "{\"kind\": \"local\", \"namePattern\": \"Save as PDF\"}");
            chromeOptions.AddUserProfilePreference("printing.print_preview_sticky_settings.appState", "{\"recentDestinations\": [{\"id\": \"Save as PDF\", \"origin\": \"local\", \"account\": \"\" }],\"version\":2,\"isGcpPromoDismissed\":false,\"selectedDestinationId\":\"Save as PDF\"}");



            chromeOptions.AddArgument("--disable-notifications");
            IWebDriver webDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions, TimeSpan.FromMinutes(3));
            webDriver.Manage().Window.Maximize();

            webDriver.Navigate().GoToUrl("https://consultaweb.ant.gob.ec/PortalWEB/paginas/clientes/clp_criterio_consulta.jsp");
            Thread.Sleep(5000);
            try
            {
               
                 if (!string.IsNullOrEmpty(cedulaPlacaDTO.Placa))
                {

                    var selectanio = new SelectElement(webDriver.FindElement(By.Id("ps_tipo_identificacion")));
                    selectanio.SelectByValue("PLA");
                    Thread.Sleep(200);
                    webDriver.FindElement(By.Id("ps_identificacion")).SendKeys(cedulaPlacaDTO.Placa);
                    Thread.Sleep(200);


                }
                
                //< option value = "CED" selected = "" > CÉDULA IDENTIDAD </ option >


                //     < option value = "PLA" > PLACA </ option >


                IWebElement btnc2 = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10))
                     .Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/form/div/a")));

                btnc2.Click();
                Thread.Sleep(4000);


                if (!string.IsNullOrEmpty(cedulaPlacaDTO.Placa))
                {
                    IWebElement IFrameDivPuntos = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10))
                     .Until(ExpectedConditions.ElementExists(By.XPath("/html/body/table[1]")));
                    Thread.Sleep(900);
                    IWebElement tablaElemento = IFrameDivPuntos.FindElement(By.TagName("tbody"));
                    Thread.Sleep(900);
                    // Encontrar todos los elementos 'td' dentro del elemento de tabla
                    var elementosTd = tablaElemento.FindElements(By.TagName("td"));
                    List<string> lIFrameDivPuntos = new List<string>();
                    Thread.Sleep(900); // Recorrer los elementos 'td' y extraer los datos
                    foreach (var elementoTd in elementosTd)
                    {

                        string valor = elementoTd.Text;

                        lIFrameDivPuntos.Add(valor);

                    }
                    lIFrameDivPuntos.RemoveAll(dato => dato == "");
                    lIFrameDivPuntos.RemoveAt(0);
                    for (int i = lIFrameDivPuntos.Count - 1; i >= 0; i--)
                    {
                        if (i % 2 == 0)
                        {
                            lIFrameDivPuntos.RemoveAt(i);
                        }
                    }
                    Thread.Sleep(900);
                    AntValoresPLacaDTO antValoresPLacaDTO = new AntValoresPLacaDTO();
                    antValoresPLacaDTO.Marca = lIFrameDivPuntos[0];
                    antValoresPLacaDTO.Color = lIFrameDivPuntos[1];
                    antValoresPLacaDTO.AnioMatrícula = lIFrameDivPuntos[2];
                    antValoresPLacaDTO.Modelo = lIFrameDivPuntos[3];
                    antValoresPLacaDTO.Clase = lIFrameDivPuntos[4];
                    antValoresPLacaDTO.FechaMatricula = lIFrameDivPuntos[5];
                    antValoresPLacaDTO.Ani = lIFrameDivPuntos[6];
                    antValoresPLacaDTO.Servicio = lIFrameDivPuntos[7];
                    antValoresPLacaDTO.FechaCaducidad = lIFrameDivPuntos[8];
                    antValoresPLacaDTO.Polarizado = lIFrameDivPuntos[9];


                    IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                    Thread.Sleep(2000);
                    js.ExecuteScript("window.localStorage.clear();");
                    Thread.Sleep(100);
                    js.ExecuteScript("window.sessionStorage.clear();");
                    Thread.Sleep(100);

                    webDriver.Manage().Cookies.DeleteAllCookies();
                    Thread.Sleep(100);
                    webDriver.Close();
                    webDriver.Quit();
                    return antValoresPLacaDTO;

                }

                return null;
                
            }
            catch { return null; }
        }



    }
}
