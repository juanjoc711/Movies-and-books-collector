﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Back;
using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Front
{
    public partial class Books : Form
    {
        private const string ApiBaseUrl = "https://localhost:44366/api/books";
        private readonly HttpClient httpClient;
        public Books()
        {
            InitializeComponent();
            httpClient = new HttpClient();
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtén los datos del formulario
                string title = txtTitle.Text;
                string author = txtAuthor.Text;
                int year = Convert.ToInt32(txtYear.Text);

                // Crea un objeto book con los datos del formulario
                Book newBook = new Book
                {
                    Title = title,
                    Author = author,
                    Year = year
                };

                // Convierte el objeto book a JSON
                string jsonBody = JsonConvert.SerializeObject(newBook);

                // Realiza la solicitud HTTP POST al servidor
                var response = await httpClient.PostAsync(ApiBaseUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

                // Maneja la respuesta del servidor según sea necesario
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Libro añadido correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error al añadir libro. Código de estado: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear película: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtén el nombre de la película a buscar
                string bookTitleToSearch = txtTitle.Text;

                // Construye la URL completa para la búsqueda por nombre
                string searchUrl = $"{ApiBaseUrl}/bytitle/{bookTitleToSearch}";

                // Realiza la solicitud HTTP GET al servidor
                var response = await httpClient.GetStringAsync(searchUrl);

                // Deserializa la respuesta JSON a una lista de objetos book
                List<Book> books = JsonConvert.DeserializeObject<List<Book>>(response);

                // Muestra los datos en el DataGridView
                dataBooks.DataSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar libro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtén el nombre de la película a eliminar
                string bookTitleToDelete = txtTitle.Text;

                // Construye la URL completa para la búsqueda por nombre
                string searchUrl = $"{ApiBaseUrl}?title={bookTitleToDelete}";

                // Realiza la solicitud HTTP GET al servidor para obtener la primera película con ese nombre
                var response = await httpClient.GetStringAsync(searchUrl);

                // Deserializa la respuesta JSON a una lista de objetos book
                List<Book> books = JsonConvert.DeserializeObject<List<Book>>(response);

                // Verifica si se encontró al menos una película con el nombre dado
                if (books.Any())
                {
                    // Obtiene el ID de la primera película encontrada
                    string bookIdToDelete = books.First().Id.ToString();

                    // Construye la URL completa para la eliminación por ID
                    string deleteUrl = $"{ApiBaseUrl}/{bookIdToDelete}";

                    // Realiza la solicitud HTTP DELETE al servidor
                    var deleteResponse = await httpClient.DeleteAsync(deleteUrl);

                    // Maneja la respuesta del servidor según sea necesario
                    if (deleteResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Libro eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar libro. Código de estado: {deleteResponse.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"No se encontró ninguna libro con el nombre {bookTitleToDelete}.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar libro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnShowAllBooks_Click(object sender, EventArgs e)
        {
            try
            {
                // Construye la URL completa para obtener todas las películas
                string getAllUrl = $"{ApiBaseUrl}";

                // Realiza la solicitud HTTP GET al servidor
                var response = await httpClient.GetStringAsync(getAllUrl);

                // Deserializa la respuesta JSON a una lista de objetos Movie
                List<Book> books = JsonConvert.DeserializeObject<List<Book>>(response);


                // Muestra los datos en el DataGridView
                dataBooks.DataSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener todas las películas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtén los datos del formulario
                string bookIdToUpdate = txtId.Text;
                string title = txtTitle.Text;
                int year = Convert.ToInt32(txtYear.Text);
                string author = txtAuthor.Text;

                // Crea un objeto Movie con los datos del formulario
                Book updatedBook = new Book
                {
                    Id = bookIdToUpdate, // El ID debe ser una cadena para esta implementación
                    Title = title,
                    Author = author,
                    Year = year
                };

                // Convierte el objeto Movie a JSON
                string jsonBody = JsonConvert.SerializeObject(updatedBook);

                // Construye la URL completa para la actualización por ID
                string updateUrl = $"{ApiBaseUrl}/{bookIdToUpdate}";

                // Realiza la solicitud HTTP PUT al servidor
                var response = await httpClient.PutAsync(updateUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

                // Maneja la respuesta del servidor según sea necesario
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Película actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error al actualizar película. Código de estado: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar película: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuPrincipal bc = new MenuPrincipal();
            bc.Show();
        }

        private void Books_Load(object sender, EventArgs e)
        {
            userName1.Text = Login.setValueUserName;
            if (userName1.Text == "Admin")
            {
                btnUpdate.Visible = true;
                btnCrear.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
                btnCrear.Visible = false;
                btnDelete.Visible = false;
            }
        }
    }
}
 
