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
using Back;
using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Front
{
    public partial class CRUD : Form
    {
        private const string ApiBaseUrl = "https://localhost:44366/api/movies";
        private readonly HttpClient httpClient;
        public CRUD()
        {
            InitializeComponent();
            httpClient = new HttpClient();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtén los datos del formulario
                string title = txtTitle.Text;
                int year = Convert.ToInt32(txtYear.Text);
                string director = txtDirector.Text;
                string genre = txtGenre.Text;
                string language = txtLanguage.Text;
                string synopsis = txtSynopsis.Text;

                // Crea un objeto Movie con los datos del formulario
                Movie newMovie = new Movie
                {
                    Title = title,
                    Year = year,
                    Director = director,
                    Genre = genre,
                    Language = language,
                    Synopsis = synopsis
                };

                // Convierte el objeto Movie a JSON
                string jsonBody = JsonConvert.SerializeObject(newMovie);

                // Realiza la solicitud HTTP POST al servidor
                var response = await httpClient.PostAsync(ApiBaseUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

                // Maneja la respuesta del servidor según sea necesario
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Película creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error al crear película. Código de estado: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear película: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtén el nombre de la película a buscar
                string movieTitleToSearch = txtTitle.Text;

                // Construye la URL completa para la búsqueda por nombre
                string searchUrl = $"{ApiBaseUrl}/bytitle/{movieTitleToSearch}";

                // Realiza la solicitud HTTP GET al servidor
                var response = await httpClient.GetStringAsync(searchUrl);

                // Deserializa la respuesta JSON a una lista de objetos book
                List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(response);

                // Muestra los datos en el DataGridView
                dataMovies.DataSource = movies;
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
                string movieTitleToDelete = txtTitle.Text;

                // Construye la URL completa para la búsqueda por nombre
                string searchUrl = $"{ApiBaseUrl}?title={movieTitleToDelete}";

                // Realiza la solicitud HTTP GET al servidor para obtener la primera película con ese nombre
                var response = await httpClient.GetStringAsync(searchUrl);

                // Deserializa la respuesta JSON a una lista de objetos Movie
                List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(response);

                // Verifica si se encontró al menos una película con el nombre dado
                if (movies.Any())
                {
                    // Obtiene el ID de la primera película encontrada
                    string movieIdToDelete = movies.First().Id.ToString();

                    // Construye la URL completa para la eliminación por ID
                    string deleteUrl = $"{ApiBaseUrl}/{movieIdToDelete}";

                    // Realiza la solicitud HTTP DELETE al servidor
                    var deleteResponse = await httpClient.DeleteAsync(deleteUrl);

                    // Maneja la respuesta del servidor según sea necesario
                    if (deleteResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Película eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar película. Código de estado: {deleteResponse.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"No se encontró ninguna película con el nombre {movieTitleToDelete}.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar película: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                // Construye la URL completa para obtener todas las películas
                string getAllUrl = $"{ApiBaseUrl}";

                // Realiza la solicitud HTTP GET al servidor
                var response = await httpClient.GetStringAsync(getAllUrl);

                // Deserializa la respuesta JSON a una lista de objetos Movie
                List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(response);


                // Muestra los datos en el DataGridView
                dataMovies.DataSource = movies;
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
                    string movieIdToUpdate = txtId.Text;
                    string title = txtTitle.Text;
                    int year = Convert.ToInt32(txtYear.Text);
                    string director = txtDirector.Text;
                    string genre = txtGenre.Text;
                    string language = txtLanguage.Text;
                    string synopsis = txtSynopsis.Text;

                    // Crea un objeto Movie con los datos del formulario
                    Movie updatedMovie = new Movie
                    {
                        Id = movieIdToUpdate,
                        Title = title,
                        Year = year,
                        Director = director,
                        Genre = genre,
                        Language = language,
                        Synopsis = synopsis
                    };

                    // Convierte el objeto Movie a JSON
                    string jsonBody = JsonConvert.SerializeObject(updatedMovie);

                    // Construye la URL completa para la actualización por ID
                    string updateUrl = $"{ApiBaseUrl}/{movieIdToUpdate}";

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

        private async void CRUD_Load(object sender, EventArgs e)
        {
            userName1.Text = Login.setValueUserName;
            if (userName1.Text == "Admin")
            {
                btnUpdate.Visible = true;
                btnCreate.Visible = true;
                btnDelete.Visible = true;
            }
            else 
            { 
                btnUpdate.Visible =false;
                btnCreate.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuPrincipal bc = new MenuPrincipal();
            bc.Show();
        }
    }
}
