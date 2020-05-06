using ManagementProject.Model;
using System.Collections.ObjectModel;
using ManagementProject.UserControls.AlarmControls;
using System.Linq;
using MangoApi;

namespace ManagementProject.ViewModel
{
   public class MonitorEquipmentViewModel : MonitorEquipmentModel
    {
        public MonitorEquipmentViewModel()
        {          
            //GetWaterDevice();
        }

        public void GetWaterDevice(int sid, Alarm[] list)
        {
            MangoApi.ElementDevice[] waterDevices = HttpAPi.GetWaterDevice();
            MangoApi.ElementDevice[] waterDevice = waterDevices.Where(x => x.mapId == sid).ToArray();
            if (waterDevice != null)
            {
                if (waterDevice[0].childern != null)
                {
                    ListBoxItems = new ObservableCollection<ContentItems>();
                    foreach (var item in waterDevice[0].childern)
                    {
                        ContentItems contentItems = new ContentItems();
                        if (item.childElements != null)
                        {
                            if (item.childElements.Length != 0)
                            {
                                contentItems.DeviceItems = new ObservableCollection<DeviceItemsInfo>();
                                DeviceItemsInfo roomInfo = new DeviceItemsInfo()
                                {
                                    IsShowRoomName = true,
                                    IsShowContant = false,
                                    RoomName = item.name,
                                    AbnormalInfo = AbnormalInfo.Normal,
                                };
                                contentItems.DeviceItems.Add(roomInfo);
                                foreach (var child in item.childElements)
                                {
                                    DeviceItemsInfo itemsInfo = new DeviceItemsInfo()
                                    {
                                        DeviceType = child.deviceTypeCode,
                                        DeviceTypeRemark = child.name,

                                    };
                                    Alarm[] select = list.Where(x => x.sersor == child.code).ToArray();
                                    if (int.Parse(child.deviceStatus) != 0)
                                    {
                                        itemsInfo.AbnormalInfo = AbnormalInfo.Abnormal;
                                    }
                                    else
                                    {
                                        if (select.Length != 0)
                                        {
                                            itemsInfo.AbnormalInfo = AbnormalInfo.Abnormal;
                                        }
                                        else
                                        {
                                            itemsInfo.AbnormalInfo = AbnormalInfo.Normal;
                                        }
                                    }
                                    contentItems.DeviceItems.Add(itemsInfo);
                                }
                            }
                        }

                        ListBoxItems.Add(contentItems);
                    }
                }
            }
        }

        public void GetGasDevice(int mapid,int sid,Alarm[] list)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetMap;
            MangoApi.MangoMap[] map = HttpAPi.GetMapList(url);
            MangoApi.MangoMap[] results = map.Where(x => x.id == mapid).ToArray();
            string deviceurl = AppConfig.ServerBaseUri + AppConfig.GetGasDevice;
            MangoApi.ElementDevice[] gasDevices = HttpAPi.GetRoomDevice(results[0].pid, deviceurl);
            if (gasDevices != null)
            {
                ListBoxItems = new ObservableCollection<ContentItems>();
                foreach (var floor in gasDevices)
                {
                    if (floor.childElements != null)
                    {
                        foreach (var room in floor.childElements)
                        {

                            if (room.childElements != null)
                            {
                                ContentItems contentItems = new ContentItems();
                                contentItems.DeviceItems = new ObservableCollection<DeviceItemsInfo>();
                                DeviceItemsInfo roomInfo = new DeviceItemsInfo()
                                {
                                    IsShowRoomName = true,
                                    IsShowContant = false,
                                    RoomName = room.roomNum,
                                    AbnormalInfo = AbnormalInfo.Normal,
                                };
                                contentItems.DeviceItems.Add(roomInfo);
                                foreach (var item in room.childElements)
                                {

                                    DeviceItemsInfo itemsInfo = new DeviceItemsInfo();
                                    Alarm[] select = list.Where(x => x.sersor == item.code).ToArray();
                                    if (item.deviceStatus != null)
                                    {
                                        if (int.Parse(item.deviceStatus) != 0)
                                        {
                                            itemsInfo.AbnormalInfo = AbnormalInfo.Abnormal;
                                        }else
                                        {                                           
                                            if (select.Length !=0)
                                            {
                                                itemsInfo.AbnormalInfo = AbnormalInfo.Abnormal;
                                            }else
                                            {
                                                itemsInfo.AbnormalInfo = AbnormalInfo.Normal;
                                            }
                                        }
                                    }else
                                    {
                                        if (select.Length != 0)
                                        {
                                            itemsInfo.AbnormalInfo = AbnormalInfo.Abnormal;
                                        }
                                        else
                                        {
                                            itemsInfo.AbnormalInfo = AbnormalInfo.Normal;
                                        }
                                    }
                                    itemsInfo.DeviceType = item.deviceTypeCode;
                                    itemsInfo.DeviceTypeRemark = item.name;
                                    contentItems.DeviceItems.Add(itemsInfo);
                                }
                                ListBoxItems.Add(contentItems);
                            }

                        }
                    }
                }
            }
        }

        private async void RefreshState()
        {
            while (true)
            {
                await System.Threading.Tasks.Task.Delay(1000);
            }
        }

        public void GetElectricDevice(int mapid, int sid, Alarm[] list)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetMap;
            MangoApi.MangoMap[] map = HttpAPi.GetMapList(url);
            MangoApi.MangoMap[] results = map.Where(x => x.id == mapid).ToArray();
            string deviceurl = AppConfig.ServerBaseUri + AppConfig.GetElectricDevice ;
            MangoApi.ElementDevice[] electricDevices = HttpAPi.GetRoomDevice(results[0].pid, deviceurl);
            if (electricDevices != null)
            {
                ListBoxItems = new ObservableCollection<ContentItems>();
                foreach (var floor in electricDevices)
                {
                    if (floor.childElements != null)
                    {
                        foreach (var room in floor.childElements)
                        {

                            if (room.childElements != null)
                            {
                                ContentItems contentItems = new ContentItems();
                                contentItems.DeviceItems = new ObservableCollection<DeviceItemsInfo>();
                                DeviceItemsInfo roomInfo = new DeviceItemsInfo()
                                {
                                    IsShowRoomName = true,
                                    IsShowContant = false,
                                    RoomName = room.roomNum,
                                    AbnormalInfo = AbnormalInfo.Normal,
                                };
                                contentItems.DeviceItems.Add(roomInfo);
                                foreach (var item in room.childElements)
                                {

                                    DeviceItemsInfo itemsInfo = new DeviceItemsInfo();
                                    Alarm[] select = list.Where(x => x.sersor == item.code).ToArray();
                                    if (item.deviceStatus != null)
                                    {
                                        if (int.Parse(item.deviceStatus) != 0)
                                        {
                                            itemsInfo.AbnormalInfo = AbnormalInfo.Abnormal;
                                        }
                                        else
                                        {
                                            if (select.Length != 0)
                                            {
                                                itemsInfo.AbnormalInfo = AbnormalInfo.Abnormal;
                                            }
                                            else
                                            {
                                                itemsInfo.AbnormalInfo = AbnormalInfo.Normal;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (select.Length != 0)
                                        {
                                            itemsInfo.AbnormalInfo = AbnormalInfo.Abnormal;
                                        }
                                        else
                                        {
                                            itemsInfo.AbnormalInfo = AbnormalInfo.Normal;
                                        }
                                    }
                                    itemsInfo.DeviceType = item.deviceTypeCode;
                                    itemsInfo.DeviceTypeRemark = item.name;
                                    contentItems.DeviceItems.Add(itemsInfo);
                                }
                                ListBoxItems.Add(contentItems);
                            }

                        }
                    }
                }
            }
        }
    }
}
