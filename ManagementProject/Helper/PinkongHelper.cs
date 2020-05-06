using com.mango.protocol;
using com.mango.protocol.msg;
using MangoApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementProject.Helper
{
    public class PinkongHelper
    {
        #region Private Field
        private static readonly CS_Pinkong cs_pinkong = new CS_Pinkong() { protocol = com.mango.protocol.Enum.Protocol.CS_PINKONG };
        private static readonly CS_LUNXUN cS_LunXun = new CS_LUNXUN() { protocol = com.mango.protocol.Enum.Protocol.CS_LUNXUN };
        #endregion

        #region Defalut Constructor
        private PinkongHelper() { }
        #endregion

        #region FightControl Operation

        public static CS_Pinkong GetCS_Pinkong() => cs_pinkong;

        private static string GetParams(object value) => JsonConvert.SerializeObject(value);

        private static async Task<SC_Pinkong> OperateAsync(CS_Pinkong pinkong)
        {
            try
            {
                await Task.Run(() =>
                {
                    var message = App.mango.Async(pinkong, (short)pinkong.protocol);
                    if (message != null)
                        return (SC_Pinkong)Package.SCStruct<SC_Pinkong>(message);

                    return null;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        private static SC_Pinkong Operate(CS_Pinkong pinkong)
        {
            try
            {
                var message = App.mango.Async(pinkong, (short)pinkong.protocol);
                if (message != null)
                    return (SC_Pinkong)Package.SCStruct<SC_Pinkong>(message);

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static Task<SC_Pinkong> SetWindowAsync(WinParams winParams)
        {
            cs_pinkong.pinkongid = 1;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.SetWindow;
            cs_pinkong.@params = GetParams(winParams);
            return OperateAsync(cs_pinkong);
        }

        public static SC_Pinkong SetWindow(WinParams winParams)
        {
            cs_pinkong.pinkongid = 1;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.SetWindow;
            cs_pinkong.@params = GetParams(winParams);
            return Operate(cs_pinkong);
        }

        public static Task<SC_Pinkong> GetWindowsAsync()
        {
            cs_pinkong.pinkongid = 8;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.GetWindows;
            cs_pinkong.@params = null;
            return OperateAsync(cs_pinkong);
        }

        public static SC_Pinkong GetWindows()
        {
            cs_pinkong.pinkongid = 8;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.GetWindows;
            cs_pinkong.@params = null;
            return Operate(cs_pinkong);
        }

        public static Task<SC_Pinkong> SetCameraAsync(WinParams winParams)
        {
            cs_pinkong.pinkongid = 2;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.SetCamera;
            cs_pinkong.@params = GetParams(winParams);
            return OperateAsync(cs_pinkong);
        }

        public static SC_Pinkong SetCamera(WinParams winParams)
        {
            cs_pinkong.pinkongid = 2;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.SetCamera;
            cs_pinkong.@params = GetParams(winParams);
            return Operate(cs_pinkong);
        }

        public static Task<SC_Pinkong> SetSceneAsync(List<SceneSet> param)//有拼控用的文件，SwitchScene，没有的话SetScene
        {
            cs_pinkong.pinkongid = 9;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.SetScene;
            cs_pinkong.@params = GetParams(param);
            return OperateAsync(cs_pinkong);
        }

        public static Task<SC_Pinkong> SaveSceneAsync(Scene scene)
        {
            cs_pinkong.pinkongid = 5;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.SaveScene;
            cs_pinkong.@params = GetParams(scene);
            return OperateAsync(cs_pinkong);
        }

        public static SC_Pinkong SaveScene(Scene scene)
        {
            cs_pinkong.pinkongid = 5;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.SaveScene;
            cs_pinkong.@params = GetParams(scene);
            return Operate(cs_pinkong);
        }

        public static Task<SC_Pinkong> SwitchSceneAsync(Scene scene)
        {
            cs_pinkong.pinkongid = 6;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.SwitchScene;
            cs_pinkong.@params = GetParams(scene);
            return OperateAsync(cs_pinkong);
        }

        public static SC_Pinkong SwitchScene(Scene scene)
        {
            cs_pinkong.pinkongid = 6;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.SwitchScene;
            cs_pinkong.@params = GetParams(scene);
            return Operate(cs_pinkong);
        }

        public static Task<SC_Pinkong> CloseWindowAsync(int id)
        {
            cs_pinkong.pinkongid = 3;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.CloseWindow;
            cs_pinkong.@params = GetParams(new { id });
            return OperateAsync(cs_pinkong);
        }

        public static SC_Pinkong CloseWindow(int id)
        {
            cs_pinkong.pinkongid = 3;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.CloseWindow;
            cs_pinkong.@params = GetParams(new { id });
            return Operate(cs_pinkong);
        }

        public static SC_Pinkong CloseAll()
        {
            cs_pinkong.pinkongid = 7;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.CloseAll;
            cs_pinkong.@params = null;
            return Operate(cs_pinkong);
        }

        public static Task<SC_Pinkong> CloseAllAsync()
        {
            cs_pinkong.pinkongid = 7;
            cs_pinkong.type = com.mango.protocol.Enum.Pinkongtype.CloseAll;
            cs_pinkong.@params = null;
            return OperateAsync(cs_pinkong);
        }

        /// <summary>
        /// 修改轮巡
        /// </summary>
        /// <param name="isModifyLunXun"> true 修改轮巡(新增、删除、修改、打开、关闭)，false 状态改变（能上墙到不能上,或不能上到能上）的事件 </param>
        /// <param name="canUpWall"> true 可以上墙，false 不可以上墙(修改轮巡的时候，canUpWall随意)</param>
        /// <returns></returns>
        public async static Task<SC_LUNXUN> ModifyLunXunAsync(bool isModifyLunXun, bool canUpWall)
        {
            try
            {
                var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                cS_LunXun.datetime = (long)(DateTime.Now - startTime).TotalMilliseconds;//timeStamp  
                cS_LunXun.type = isModifyLunXun ? 1 : 2;//1 修改轮巡(新增、删除、修改、打开、关闭、)，2 状态改变（能上墙到不能上或者不能上到能上）的事件
                cS_LunXun.status = canUpWall ? 1 : 2;//1 可以上墙，2 不可以上墙(修改轮巡的时候，status随意)
                await Task.Run(() =>
                {
                    var message = App.mango.Async(cS_LunXun, (short)cS_LunXun.protocol);
                    if (message != null)
                        return (SC_LUNXUN)Package.SCStruct<SC_LUNXUN>(message);

                    return null;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        #endregion

        #region Client UpWall Or DownWall
        /// <summary>
        /// 客户端上墙
        /// </summary>
        public static void ClientUpWall()
        {
            var encoderConfigs = MangoInfo.instance.EncoderConfigs;
            if (encoderConfigs != null)
            {
                ModifyLunXunAsync(false, false);
                encoderConfigs.ForEach(encoder => SetWindow(encoder));
            }
            #region 测试数据

            //var winParams = new EncoderConfig
            //{
            //    id = 255,
            //    z = 0,
            //    x = 1920,
            //    y = 0,
            //    split = 1,
            //    w = 1920,
            //    h = 1920,
            //    cam = new Camera[]
            //    {
            //        new Camera
            //        {
            //            code="n",//唯一标识
            //            ip="192.168.10.4:554",
            //            name="总控",
            //            user="admin",
            //            pass="admin12345",
            //            vendor=3,//
            //        }
            //    },
            //};
            //var sss = SetWindow(winParams);

            //var str = "{\"id\":255,\"x\":1920,\"y\":0,\"w\":1920,\"h\":1920,\"z\":0,\"split\":1,\"cam\":[{\"code\":\"192_168_10_112\",\"name\":\"JW-先进材料楼715实验室2\",\"vendor\":3,\"ip\":\"192.168.10.112:554\",\"user\":\"admin\",\"pass\":\"admin12345\"}]}";
            //var s777ss = "{\"id\":254,\"x\":0,\"y\":0,\"w\":1920,\"h\":1920,\"z\":0,\"split\":1,\"cam\":[{\"code\":\"192_168_10_4\",\"name\":\"jw-总监控\",\"vendor\":3,\"ip\":\"192.168.10.4:554\",\"user\":\"admin\",\"pass\":\"admin12345\"}]}";
            //var js = JsonConvert.DeserializeObject<EncoderConfig>(str);
            //var js2 = JsonConvert.DeserializeObject<EncoderConfig>(s777ss);

            //var lxj = SetWindow(js);
            //var lxj2 = SetWindow(js2);



            //var ddd = SwitchScene(new Scene { id = 0, name = "temp_scene" });
            #endregion
        }

        /// <summary>
        /// 客户端下墙
        /// </summary>
        public static void ClientDownWall()
        {
            var encoderConfigs = MangoInfo.instance.EncoderConfigs;
            if (encoderConfigs != null)
            {
                ModifyLunXunAsync(false, true);
                encoderConfigs.ForEach(encoder => CloseWindow(encoder.id));
            }
        }
        #endregion
    }

    #region Scene
    public class Scene
    {
        public int id;
        public string name;
    }
    #endregion
}
