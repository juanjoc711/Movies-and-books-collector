using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Front
{
    public partial class Login : Form
    {
        private const string ApiBaseUrl = "https://localhost:44366/api/users";
        private readonly HttpClient httpClient;
        public static string setValueUserName = "";
        public Login()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            txtPassword.UseSystemPasswordChar = true;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtUserName.Text;
            string password = txtPassword.Text;
            setValueUserName = txtUserName.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowErrorMessage("Por favor, complete todos los campos.");
                return;
            }
            // Verificar si las credenciales son válidas
            var userData = new { Username = username, Password = password };
            var json = JsonConvert.SerializeObject(userData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(ApiBaseUrl + "/login", content);

            if (response.IsSuccessStatusCode)
            {
                ShowSuccessMessage("Inicio de sesión exitoso");
                this.Hide();
                MenuPrincipal ab = new MenuPrincipal();
                ab.Show();

            }
            else
            {
                ShowErrorMessage("Credenciales inválidas. Por favor, inténtelo nuevamente.");
            }
        }
        private async Task<bool> UserExists(string username)
        {
            using (HttpClient client = new HttpClient())
            {
                string useraNameExist = txtUserName.Text;
                string searchUrl = $"{ApiBaseUrl}/userexists?username={useraNameExist}";

                var response = await client.GetAsync(searchUrl);

                if (response.IsSuccessStatusCode)
                {
                    bool userExists = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                    return userExists;
                }
                else
                {
                    return false;
                }
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;


            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowErrorMessage("Por favor, complete todos los campos.");
                return;
            }

            // Verificar si el usuario ya existe antes de intentar registrarse
            if (await UserExists(username))
            {
                ShowWarningMessage("El usuario ya existe. Por favor, elija otro nombre de usuario.");
                return;
            }

            // Si el usuario no existe, procede con el registro
            var userData = new { Username = username, Password = password };
            var json = JsonConvert.SerializeObject(userData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(ApiBaseUrl + "/register", content);

            if (response.IsSuccessStatusCode)
            {
                ShowSuccessMessage("Registro exitoso");
                ShowSuccessMessage("Bienvenido");
                this.Hide();
                MenuPrincipal ab = new MenuPrincipal();
                ab.Show();
            }
            else
            {
                ShowErrorMessage("Error durante el registro. Por favor, inténtelo nuevamente.");
            }
        }
        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
