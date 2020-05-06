using System.Collections.Generic;
using ManagementProject.Model;

namespace ManagementProject.Services
{
    public class ScenesService: IDataServices<Scenes>
    {
        public ICollection<Scenes> GetAllDatas()
        {
            List<Scenes> scenes = new List<Scenes>();

            for (int i = 0; i < 10; i++)
            {
                scenes.Add(new Scenes
                {
                    Id=i,
                    Name=$"场景{i}"
                });
            }

            return scenes;
        }
    }
}
