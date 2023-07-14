using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using System.Text;
using Newtonsoft.Json;

namespace directory_deleter.Models
{
    internal class ProfileModel
    {
        public string[] ProfileLocations { get; set; }

        public string[] ProfileFolders { get; set; }

        public ProfileModel(string[] locations, string[] folders)
        {
            ProfileFolders = folders;
            ProfileLocations = locations;
        }

        public async Task SaveProfile(CancellationToken token)
        {
            string val = JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
            using var stream = new MemoryStream(Encoding.Default.GetBytes(val));
            var fileSaveResult = await FileSaver.Default.SaveAsync("profile.json", stream, token);
            if (fileSaveResult.IsSuccessful)
            {
                await Toast.Make($"File is saved: {fileSaveResult.FilePath}").Show(token);
            }
            else
            {
                await Toast.Make($"File is not saved, {fileSaveResult.Exception.Message}").Show(token);
            }
        }

        public async Task<ProfileModel> LoadProfile(CancellationToken token)
        {
            ProfileModel profileObject = null;
            var result = await FilePicker.Default.PickAsync();
            if (result != null && result.ContentType.Contains("json"))
            {
                using var stream = await result.OpenReadAsync();
                StreamReader reader = new(stream);
                string text = await reader.ReadToEndAsync(token);
                profileObject = (ProfileModel)JsonConvert.DeserializeObject(text, GetType());
                await Toast.Make($"Profile loaded from {result.FileName}", ToastDuration.Long).Show(token);
            }
            else
            {
                await Toast.Make($"Please specify the right profile file").Show(token);
            }
            return profileObject;
        }
    }
}
