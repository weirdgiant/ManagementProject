using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Windows.Threading;
using System.Threading;

namespace ManagementProject
{


    public class SdkManager
    {
        public const uint EX_SUCCESS = 0;
        public const uint EX_FAILED = 0;
        public List<ORG_RES_QUERY_ITEM_S> CameraList = new List<ORG_RES_QUERY_ITEM_S>();
        public Dispatcher AlarmListViewDispatcher;

        public static string m_strDateFormat = "yyyy-MM-dd HH:mm:ss";
        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
        public AutoResetEvent isGotCamListResetEvent = new AutoResetEvent(false);
        //登录成功返回信息
        public LOGIN_INFO_S stLoginInfo;

        //用户登录参数
        public string srvIpAddr { get; set; }
        public string usrLoginName { get; set; }
        public string usrLoginPsw { get; set; }
        public bool userLoginStatus { get; set; }
        private static SdkManager sdkManager { get; set; }

        public TreeView TreeViewRoot { get; set; }
        //回调函数指针
        public IMOSSDK.CALL_BACK_PROC_PF CallBackFunc;

        public static SdkManager GetInstance()
        {
            if (sdkManager == null)
            {
                sdkManager = new SdkManager();
            }
            return sdkManager;
        }

        private SdkManager()
        {
            TreeViewRoot = new TreeView();
        }
     


        private string GetDevType(uint ulDevType)
        {
            switch (ulDevType)
            {
                case (uint)IMOS_DEVICE_TYPE_E.IMOS_DT_EC1101_HF:
                    return "EC1101_HF";
                case (uint)IMOS_DEVICE_TYPE_E.IMOS_DT_EC1501_HF:
                    return "EC1501_HF";
                case (uint)IMOS_DEVICE_TYPE_E.IMOS_DT_EC1102_HF:
                    return "EC1102_HF";
                case (uint)IMOS_DEVICE_TYPE_E.IMOS_DT_EC1801_HH:
                    return "EC1801_HH";
                case (uint)IMOS_DEVICE_TYPE_E.IMOS_DT_EC2004_HF:
                    return "EC2004_HF";
                case (uint)IMOS_DEVICE_TYPE_E.IMOS_DT_DC1001_FF:
                    return "DC1001_HF";
                case (uint)IMOS_DEVICE_TYPE_E.IMOS_DT_DC2004_FF:
                    return "DC2004_FF";
                default:
                    return "未知";
            }
        }

        private string GetOnlineStatus(uint ulStatus)
        {
            switch (ulStatus)
            {
                case IMOSSDK.IMOS_DEV_STATUS_ONLINE:
                    return "在线";
                case IMOSSDK.IMOS_DEV_STATUS_OFFLINE:
                    return "离线";
                default:
                    return "未知";
            }
        }

        public string GetCameraType(uint ulCameraType)
        {
            switch (ulCameraType)
            {
                case (uint)CAMERA_TYPE_E.CAMERA_TYPE_FIX:
                    return "标清固定摄像机";
                case (uint)CAMERA_TYPE_E.CAMERA_TYPE_PTZ:
                    return "标清云台摄像机";
                case (uint)CAMERA_TYPE_E.CAMERA_HD_TYPE_FIX:
                    return "高清固定摄像机";
                case (uint)CAMERA_TYPE_E.CAMERA_HD_TYPE_PTZ:
                    return "高清云台摄像机";
                case (uint)CAMERA_TYPE_E.CAMERA_TYPE_CAR:
                    return "车载摄像机";
                case (uint)CAMERA_TYPE_E.CAMERA_TYPE_VIRTUAL:
                    return "虚拟摄像机";
                case (uint)CAMERA_TYPE_E.CAMERA_TYPE_BALL_NOT_CONTROL:
                    return "不可控标清球机";
                case (uint)CAMERA_TYPE_E.CAMERA_HD_TYPE_BALL_NOT_CONTROL:
                    return "不可控高清球机";
                case (uint)CAMERA_TYPE_E.CAMERA_TYPE_SAFE_VM:
                    return "VM安全接入摄像机";
                case (uint)CAMERA_TYPE_E.CAMERA_TYPE_SAFE_DVR:
                    return "DVR安全接入摄像机";
                case (uint)CAMERA_TYPE_E.CAMERA_TYPE_SAFE_MATRIX:
                    return "矩阵安全接入摄像机";
                case (uint)CAMERA_TYPE_E.CAMERA_TYPE_FIX_BOX:
                    return "变焦枪机";
                case (uint)CAMERA_TYPE_E.CAMERA_TYPE_VGA:
                    return "VGA输入摄像机";

                default:
                    return "未知";
            }

        }

        private string GetPTZProtocol(int index)
        {
            switch (index)
            {
                case 0:
                    return "PELCO-D";
                case 1:
                    return "PELCO-P";
                case 2:
                    return "ALEC";
                case 3:
                    return "VISCA";
                case 4:
                    return "ALEC_PELCO-D";
                case 5:
                    return "ALEC_PELCO-P";
                case 6:
                    return "MINKING_PELCO-D";
                case 7:
                    return "MINKING_PELCO-P";
                default:
                    return "未知";
            }
        }

        public List<EC_QUERY_ITEM_S> GetECList(string OrgCode)
        {
            try
            {
                uint ulRet = 0;
                uint ulBeginNum = 0;
                uint ulTotalNum = 0;

                RSP_PAGE_INFO_S stRspPageInfo;
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();

                IntPtr ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));
                IntPtr ptrECList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(EC_QUERY_ITEM_S)) * 30);

                List<EC_QUERY_ITEM_S> list = new List<EC_QUERY_ITEM_S>();
                IntPtr ptrQueryCondition = new IntPtr(0);

                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 0;

                    ulRet = IMOSSDK.IMOS_QueryEcList(ref IMOSSDK.stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(OrgCode), ptrQueryCondition, ref stPageInfo, ptrRspPage, ptrECList);
                    if (0 != ulRet)
                    {
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    EC_QUERY_ITEM_S stECItem;
                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrECList.ToInt64() + Marshal.SizeOf(typeof(EC_QUERY_ITEM_S)) * i);
                        stECItem = (EC_QUERY_ITEM_S)Marshal.PtrToStructure(ptrTemp, typeof(EC_QUERY_ITEM_S));
                        list.Add(stECItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                return null;
            }
        }
        public List<DC_QUERY_ITEM_S> GetDCList(string OrgCode)
        {
            try
            {
                uint ulRet = 0;
                uint ulBeginNum = 0;
                uint ulTotalNum = 0;

                RSP_PAGE_INFO_S stRspPageInfo;
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();

                IntPtr ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));
                IntPtr ptrDCList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DC_QUERY_ITEM_S)) * 30);

                List<DC_QUERY_ITEM_S> list = new List<DC_QUERY_ITEM_S>();
                IntPtr ptrQueryCondition = new IntPtr(0);

                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 0;

                    ulRet = IMOSSDK.IMOS_QueryDcList(ref IMOSSDK.stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(OrgCode), ptrQueryCondition, ref stPageInfo, ptrRspPage, ptrDCList);
                    if (0 != ulRet)
                    {
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    DC_QUERY_ITEM_S stDCItem;
                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrDCList.ToInt64() + Marshal.SizeOf(typeof(DC_QUERY_ITEM_S)) * i);
                        stDCItem = (DC_QUERY_ITEM_S)Marshal.PtrToStructure(ptrTemp, typeof(DC_QUERY_ITEM_S));
                        list.Add(stDCItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public List<CAM_AND_CHANNEL_QUERY_ITEM_S> GetECChanelList(string ECCode)
        {
            try
            {
                uint ulRet = 0;
                uint ulBeginNum = 0;
                uint ulTotalNum = 0;

                RSP_PAGE_INFO_S stRspPageInfo;
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();

                IntPtr ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));
                IntPtr ptrECChanelList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CAM_AND_CHANNEL_QUERY_ITEM_S)) * 30);

                List<CAM_AND_CHANNEL_QUERY_ITEM_S> list = new List<CAM_AND_CHANNEL_QUERY_ITEM_S>();
                IntPtr ptrQueryCondition = new IntPtr(0);

                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 0;

                    ulRet = IMOSSDK.IMOS_QueryCameraAndChannelList(ref IMOSSDK.stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ECCode), ptrQueryCondition, ref stPageInfo, ptrRspPage, ptrECChanelList);
                    if (0 != ulRet)
                    {
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    CAM_AND_CHANNEL_QUERY_ITEM_S stECChanelItem;
                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrECChanelList.ToInt64() + Marshal.SizeOf(typeof(CAM_AND_CHANNEL_QUERY_ITEM_S)) * i);
                        stECChanelItem = (CAM_AND_CHANNEL_QUERY_ITEM_S)Marshal.PtrToStructure(ptrTemp, typeof(CAM_AND_CHANNEL_QUERY_ITEM_S));
                        list.Add(stECChanelItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

                return null;
            }
        }
        public List<SCR_AND_CHANNEL_QUERY_ITEM_S> GetDCChanelList(string DCCode)
        {
            try
            {
                uint ulRet = 0;
                uint ulBeginNum = 0;
                uint ulTotalNum = 0;

                RSP_PAGE_INFO_S stRspPageInfo;
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();

                IntPtr ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));
                IntPtr ptrDCChanelList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SCR_AND_CHANNEL_QUERY_ITEM_S)) * 30);

                List<SCR_AND_CHANNEL_QUERY_ITEM_S> list = new List<SCR_AND_CHANNEL_QUERY_ITEM_S>();
                IntPtr ptrQueryCondition = new IntPtr(0);

                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 0;

                    ulRet = IMOSSDK.IMOS_QueryScreenAndChannelList(ref IMOSSDK.stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(DCCode), ptrQueryCondition, ref stPageInfo, ptrRspPage, ptrDCChanelList);
                    if (0 != ulRet)
                    {
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    SCR_AND_CHANNEL_QUERY_ITEM_S stDCChanelItem;
                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrDCChanelList.ToInt64() + Marshal.SizeOf(typeof(SCR_AND_CHANNEL_QUERY_ITEM_S)) * i);
                        stDCChanelItem = (SCR_AND_CHANNEL_QUERY_ITEM_S)Marshal.PtrToStructure(ptrTemp, typeof(SCR_AND_CHANNEL_QUERY_ITEM_S));
                        list.Add(stDCChanelItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public void DeleteEC(byte[] ECCode)
        {
            uint ulRet = 0;
            ulRet = IMOSSDK.IMOS_DelEc(ref IMOSSDK.stLoginInfo.stUserLoginIDInfo, ECCode);
            if (0 != ulRet)
            {
                return;
            }

        }

        public void DeleteDC(byte[] DCCode)
        {
            uint ulRet = 0;
            ulRet = IMOSSDK.IMOS_DelDc(ref IMOSSDK.stLoginInfo.stUserLoginIDInfo, DCCode);
            if (0 != ulRet)
            {
                return;
            }

        }


        private List<ORG_RES_QUERY_ITEM_S> GetDomain(TreeNode treeNode)
        {
            IntPtr ptrResList = IntPtr.Zero;
            IntPtr ptrRspPage = IntPtr.Zero;
            try
            {
                uint ulRet = 0;
                uint ulBeginNum = 0;
                uint ulTotalNum = 0;
                ORG_RES_QUERY_ITEM_S st = (ORG_RES_QUERY_ITEM_S)treeNode.Tag;

                ptrResList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * 30);
                ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));

                RSP_PAGE_INFO_S stRspPageInfo;
                List<ORG_RES_QUERY_ITEM_S> list = new List<ORG_RES_QUERY_ITEM_S>();
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 1;

                    ulRet = IMOSSDK.IMOS_QueryResourceList(ref stLoginInfo.stUserLoginIDInfo, st.szResCode, (uint)IMOS_TYPE_E.IMOS_TYPE_ORG, 0, ref stPageInfo, ptrRspPage, ptrResList);
                    if (0 != ulRet)
                    {
#if DEBUG
                        MessageBox.Show("IMOS_QueryResourceList获取资源列表失败！错误码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                        //Logger.Error(typeof(SdkManager), "IMOS_QueryResourceList获取资源列表失败！错误码：" + ErrorCode.GetErrorMsg(ulRet));
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    ORG_RES_QUERY_ITEM_S stOrgResItem;
                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrResList.ToInt64() + Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * i);
                        stOrgResItem = (ORG_RES_QUERY_ITEM_S)Marshal.PtrToStructure(ptrTemp, typeof(ORG_RES_QUERY_ITEM_S));
                        list.Add(stOrgResItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;

            }
            catch (Exception ex)
            {
               // Logger.Error(typeof(SdkManager), ex);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(ptrResList);
                Marshal.FreeHGlobal(ptrRspPage);
            }
        }

        /// <summary>
        /// 获取指定节点下的摄像机数据列表。
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        private List<ORG_RES_QUERY_ITEM_S> GetCamera(TreeNode treeNode)
        {
            try
            {
                uint ulRet = 0;
                uint ulBeginNum = 0;
                uint ulTotalNum = 0;
                ORG_RES_QUERY_ITEM_S st = (ORG_RES_QUERY_ITEM_S)treeNode.Tag;

                IntPtr ptrResList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * 50);
                IntPtr ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));

                RSP_PAGE_INFO_S stRspPageInfo;
                List<ORG_RES_QUERY_ITEM_S> list = new List<ORG_RES_QUERY_ITEM_S>();
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 50;
                    stPageInfo.bQueryCount = 1;
                    ulRet = IMOSSDK.IMOS_QueryResourceList(ref stLoginInfo.stUserLoginIDInfo, st.szResCode, (uint)IMOS_TYPE_E.IMOS_TYPE_CAMERA, 0, ref stPageInfo, ptrRspPage, ptrResList);

                    if (0 != ulRet)
                    {
#if DEBUG
                        //Todo:uncomment
                        //MsgBox.Show("IMOS_QueryResourceList获取资源列表失败！错误码：" + ErrorCode.GetErrorMsg(ulRet)+ treeNode.Text);

#endif
                       // Logger.Error(typeof(SdkManager), treeNode.Text + "IMOS_QueryResourceList获取资源列表失败！错误码：" + ErrorCode.GetErrorMsg(ulRet));
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;
                    ORG_RES_QUERY_ITEM_S stOrgResItem;

                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrResList.ToInt64() + Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * i);
                        stOrgResItem = (ORG_RES_QUERY_ITEM_S)Marshal.PtrToStructure(ptrTemp, typeof(ORG_RES_QUERY_ITEM_S));
                        list.Add(stOrgResItem);
                    }

                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;

            }
            catch (Exception ex)
            {
               // Logger.Error(typeof(SdkManager), ex);
                return null;
            }
        }

        /// <summary>
        /// 构造树
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="treeView"></param>
        public void BuildTree(TreeNode treeNode, TreeView treeView)
        {
            try
            {
                treeView.BeginUpdate();
                //获取域数据
                List<ORG_RES_QUERY_ITEM_S> list = GetDomain(treeNode);

                if (null != list)
                {
                    foreach (ORG_RES_QUERY_ITEM_S org in list)
                    {
                        TreeNode domainNode = new TreeNode();
                        domainNode.Text = IMOSSDK.UTF8ToUnicode(org.szResName);
                        domainNode.Tag = org;
                        domainNode.ImageIndex = 0;
                        domainNode.SelectedImageIndex = 0;
                        treeNode.Nodes.Add(domainNode);
                        BuildTree(domainNode, treeView);
                    }
                }
                treeNode.ExpandAll();
                //获取摄像机数据
                List<ORG_RES_QUERY_ITEM_S> cameraList = GetCamera(treeNode);
                if(cameraList==null)
                {
                    return;
                }
                foreach (ORG_RES_QUERY_ITEM_S camera in cameraList)
                {
                    TreeNode cameraNode = new TreeNode();
                    cameraNode.Text = IMOSSDK.UTF8ToUnicode(camera.szResName);
                    cameraNode.Tag = camera;
                    //if (camera.ulResStatus == IMOSSDK.IMOS_DEV_STATUS_ONLINE)
                    //{
                    CameraList.Add(camera);
                    //}

                    if (2 == camera.ulResSubType || 4 == camera.ulResSubType)
                    {
                        //云台摄像机
                        switch (camera.ulResStatus)
                        {
                            case IMOSSDK.IMOS_DEV_STATUS_ONLINE:
                                cameraNode.ImageIndex = 1;
                                cameraNode.SelectedImageIndex = 1;
                                break;
                            case IMOSSDK.IMOS_DEV_STATUS_OFFLINE:
                                cameraNode.ImageIndex = 2;
                                cameraNode.SelectedImageIndex = 2;
                                break;
                            default:
                                cameraNode.ImageIndex = 2;
                                cameraNode.SelectedImageIndex = 2;
                                break;
                        }
                    }
                    else
                    {
                        switch (camera.ulResStatus)
                        {
                            case IMOSSDK.IMOS_DEV_STATUS_ONLINE:
                                cameraNode.ImageIndex = 4;
                                cameraNode.SelectedImageIndex = 4;
                                break;
                            case IMOSSDK.IMOS_DEV_STATUS_OFFLINE:
                                cameraNode.ImageIndex = 5;
                                cameraNode.SelectedImageIndex = 5;
                                break;
                            default:
                                cameraNode.ImageIndex = 4;
                                cameraNode.SelectedImageIndex = 4;
                                break;
                        }
                    }

                    treeNode.Nodes.Add(cameraNode);
                }
            }
            catch (Exception ex)
            {

              //  Logger.Error(typeof(SdkManager), ex);
            }
            finally
            {
                treeView.EndUpdate();
            }
        }

        /// <summary>
        /// 设置根节点
        /// </summary>
        public void SetTreeRoot(TreeView treeView)
        {
            try
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = IMOSSDK.UTF8ToUnicode(stLoginInfo.szDomainName);
                ORG_RES_QUERY_ITEM_S stOrgQueryItem = new ORG_RES_QUERY_ITEM_S();
                stOrgQueryItem.szOrgCode = stLoginInfo.szOrgCode;
                stOrgQueryItem.szResName = stLoginInfo.szDomainName;
                stOrgQueryItem.szResCode = stLoginInfo.szOrgCode;
                stOrgQueryItem.ulResType = (uint)IMOS_TYPE_E.IMOS_TYPE_ORG;
                treeNode.Tag = stOrgQueryItem;
                treeView.Nodes.Add(treeNode);
                treeView.ExpandAll();

            }
            catch (Exception ex)
            {
             //   Logger.Error(typeof(SdkManager), ex);

            }
        }

        /// <summary>
        /// 根据当前节点，显示该节点下的子节点。
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="treeView"></param>
        public void OrganizeChildrenNodes(TreeNode parentNode, TreeView treeView)
        {
            if (null == parentNode)
            {
                return;
            }
            try
            {
                ORG_RES_QUERY_ITEM_S stOrgQueryItem = (ORG_RES_QUERY_ITEM_S)parentNode.Tag;
                //ORG_RES_QUERY_ITEM_S stOrgQueryItem = (ORG_RES_QUERY_ITEM_S)parentNode.Tag;

                if (stOrgQueryItem.ulResType == (uint)IMOS_TYPE_E.IMOS_TYPE_ORG)
                {
                    parentNode.Nodes.Clear();
                    BuildTree(parentNode, treeView);
                }
            }
            catch (Exception ex)
            {
              //  Logger.Error(typeof(SdkManager), ex);
            }

        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <returns></returns>
        public bool LoginMethod(string usrLoginName, string usrLoginPsw, string srvIpAddr, string cltIpAddr)
        {

            this.usrLoginName = usrLoginName;
            this.usrLoginPsw = usrLoginPsw;
            this.srvIpAddr = srvIpAddr;
            uint srvPort = 8800;

            //1.初始化
            var ulRet = IMOSSDK.IMOS_Initiate("N/A", srvPort, 1, 1);
            if (0 != ulRet)
            {
                MessageBox.Show("初始化失败：" + ErrorCode.GetErrorMsg(ulRet));
                Logger.Error("初始化失败：" + ErrorCode.GetErrorMsg(ulRet));
                return false;
            }

            //2.加密密码
            IntPtr ptr_MD_Pwd = Marshal.AllocHGlobal(sizeof(char) * IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN);
            ulRet = IMOSSDK.IMOS_Encrypt(usrLoginPsw, (uint)usrLoginPsw.Length, ptr_MD_Pwd);

            if (0 != ulRet)
            {
                MessageBox.Show("登陆加密失败：" + ErrorCode.GetErrorMsg(ulRet));
                Logger.Error("登陆加密失败：" + ErrorCode.GetErrorMsg(ulRet));
                return false;
            }

            string MD_PWD = Marshal.PtrToStringAnsi(ptr_MD_Pwd);
            Marshal.FreeHGlobal(ptr_MD_Pwd);

            //3.登录方法
            IntPtr ptrLoginInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LOGIN_INFO_S)));
            ulRet = IMOSSDK.IMOS_LoginEx(usrLoginName, MD_PWD, srvIpAddr, cltIpAddr, ptrLoginInfo);
            if (0 != ulRet)
            {
                MessageBox.Show("登陆失败：" + ErrorCode.GetErrorMsg(ulRet));
                Logger.Error("登陆失败："+ErrorCode.GetErrorMsg(ulRet));
                return false;

            }

            stLoginInfo = (LOGIN_INFO_S)Marshal.PtrToStructure(ptrLoginInfo, typeof(LOGIN_INFO_S));
            Marshal.FreeHGlobal(ptrLoginInfo);

            //4.保活
            return true;
        }
        public void LogOut()
        {
            userLoginStatus = false;
            IMOSSDK.IMOS_LogoutEx(ref stLoginInfo.stUserLoginIDInfo);
            Logger.Info(typeof(SdkManager), "IMOS_LoginEx 登出！");

        }
        public void CleanUp()
        {
            uint ulRet = 0;
            IntPtr userInfoPtr = new IntPtr(0);

            ulRet = IMOSSDK.IMOS_CleanUp(userInfoPtr);
            if (ulRet != 0)
            {
                Logger.Info(typeof(SdkManager), "IMOS_CleanUp 失败");
            }
        }



        public List<RECORD_FILE_INFO_S> QueryRecord(string strBegin, string strEnd, string m_cameraCode)
        {
            List<RECORD_FILE_INFO_S> RecFileList = new List<RECORD_FILE_INFO_S>();

            try
            {
                uint ulRet = 0;
                uint ulBeginNum = 0;
                uint ulTotalNum = 0;
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();

                REC_QUERY_INFO_S stRecQueryInfo = new REC_QUERY_INFO_S();
                stRecQueryInfo.szReserve = new byte[32];

                stRecQueryInfo.szCamCode = Encoding.Default.GetBytes(m_cameraCode);
                stRecQueryInfo.stQueryTimeSlice.szBeginTime = new byte[IMOSSDK.IMOS_TIME_LEN];
                Encoding.Default.GetBytes(strBegin, 0, Encoding.Default.GetByteCount(strBegin), stRecQueryInfo.stQueryTimeSlice.szBeginTime, 0);
                stRecQueryInfo.stQueryTimeSlice.szEndTime = new byte[IMOSSDK.IMOS_TIME_LEN];
                Encoding.Default.GetBytes(strEnd, 0, Encoding.Default.GetByteCount(strEnd), stRecQueryInfo.stQueryTimeSlice.szEndTime, 0);

                IntPtr ptrRecFileList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RECORD_FILE_INFO_S)) * 30);
                IntPtr ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    ulRet = IMOSSDK.IMOS_RecordRetrieval(ref stLoginInfo.stUserLoginIDInfo, ref stRecQueryInfo, ref stPageInfo, ptrRspPage, ptrRecFileList);
                    if (0 != ulRet)
                    {
                        Logger.Info(typeof(SdkManager), "IMOS_RecordRetrieval 失败:" + ErrorCode.GetErrorMsg(ulRet));
                        return RecFileList;
                    }

                    var stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrRecFileList.ToInt64() + Marshal.SizeOf(typeof(RECORD_FILE_INFO_S)) * i);
                        var stRecFileItem = (RECORD_FILE_INFO_S)Marshal.PtrToStructure(ptrTemp, typeof(RECORD_FILE_INFO_S));
                        RecFileList.Add(stRecFileItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return RecFileList;
            }
            catch (System.Exception ex)
            {

                Logger.Info(typeof(SdkManager), ex);
                return RecFileList;
            }
        }


        /// <summary>
        /// 播放录像方法
        /// </summary>
        /// <param name="RecFileList">录像列表</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="m_cameraCode">摄像机编码</param>
        public List<URL_INFO_S> GetRecordURL(List<RECORD_FILE_INFO_S> RecFileList, string m_cameraCode)
        {

            List<URL_INFO_S> UrlList = new List<URL_INFO_S>();

            foreach (RECORD_FILE_INFO_S recordInfo in RecFileList)
            {
                string strSubBegin = Encoding.Default.GetString(recordInfo.szStartTime, 0, 19);
                string strSubEnd = Encoding.Default.GetString(recordInfo.szEndTime, 0, 19);

                IFormatProvider culture = new CultureInfo("zh-CN");

                DateTime subBegin = DateTime.ParseExact(strSubBegin, m_strDateFormat, culture);
                DateTime subEnd = DateTime.ParseExact(strSubEnd, m_strDateFormat, culture);


                GET_URL_INFO_S GetURLInfo = new GET_URL_INFO_S();
                GetURLInfo.szCamCode = Encoding.Default.GetBytes(m_cameraCode);
                GetURLInfo.szClientIp = stLoginInfo.stUserLoginIDInfo.szUserIpAddress;
                GetURLInfo.szFileName = recordInfo.szFileName;

                GetURLInfo.stRecTimeSlice.szBeginTime = new byte[IMOSSDK.IMOS_TIME_LEN];
                GetURLInfo.stRecTimeSlice.szEndTime = new byte[IMOSSDK.IMOS_TIME_LEN];

                Byte[] timeBytes = Encoding.Default.GetBytes(strSubBegin);
                timeBytes.CopyTo(GetURLInfo.stRecTimeSlice.szBeginTime, 0);
                timeBytes = Encoding.Default.GetBytes(strSubEnd);
                timeBytes.CopyTo(GetURLInfo.stRecTimeSlice.szEndTime, 0);

                URL_INFO_S urlInfo = new URL_INFO_S();
                uint ulRet = IMOSSDK.IMOS_GetRecordFileURL(ref stLoginInfo.stUserLoginIDInfo, ref GetURLInfo, ref urlInfo);

                if (0 == ulRet)
                {
                    UrlList.Add(urlInfo);
                }
                else
                {
                    Logger.Info(typeof(SdkManager), "IMOS_GetRecordFileURL 失败:" + ErrorCode.GetErrorMsg(ulRet));
                }
            }

            return UrlList;
        }

        public void SpeedPlay(byte[] szChannelCode, uint playSpeed)
        {
            uint ulRet = IMOSSDK.IMOS_SetPlaySpeed(ref stLoginInfo.stUserLoginIDInfo, szChannelCode, playSpeed);
            if (ulRet != 0)
            {
                Logger.Info(typeof(SdkManager), "IMOS_SetPlaySpeed 失败:" + ErrorCode.GetErrorMsg(ulRet));

            }
        }

        /// <summary>
        /// 设置预置位
        /// </summary>
        /// <param name="szCamCode"></param>
        /// <param name="preset"></param>
        /// <returns></returns>
        public uint SetPresetLoc(byte[] szCamCode, PRESET_INFO_S preset)
        {
            uint ulRet = 0;
            ulRet = IMOSSDK.IMOS_SetPreset(ref stLoginInfo.stUserLoginIDInfo, szCamCode, ref preset);
            if (ulRet != 0)
            {
                Logger.Info(typeof(SdkManager), "IMOS_SetPreset 失败:" + ErrorCode.GetErrorMsg(ulRet));

            }
            return ulRet;
        }

        /// <summary>
        /// 使用预置位
        /// </summary>
        /// <param name="szCamCode"></param>
        /// <param name="ulPresetNum"></param>
        /// <returns></returns>
        public uint UsePreset(byte[] szCamCode, uint ulPresetNum)
        {
            uint ulRet = 0;
            ulRet = IMOSSDK.IMOS_UsePreset(ref stLoginInfo.stUserLoginIDInfo, szCamCode, ulPresetNum);
            if (ulRet != 0)
            {
                Logger.Info(typeof(SdkManager), "IMOS_UsePreset 失败:" + ErrorCode.GetErrorMsg(ulRet));

            }
            return ulRet;
        }

        /// <summary>
        /// 回放下载
        /// </summary>
        /// <param name="recFile">录像信息</param>
        /// <param name="camCode">摄像机编码</param>
        /// <param name="downloadLoc">用户下载存储位置</param>
        /// <returns></returns>
        public byte[] StartDownLoad(RECORD_FILE_INFO_S fileInfo, byte[] camCode, XP_PROTOCOL_E vodProtocol, string downloadLoc, XP_DOWN_MEDIA_SPEED_E speed, uint downloadFormat, DateTime beginTime, DateTime endTime)
        {
            uint ulRet = 0;
            IntPtr ptrSDKURLInfo = IntPtr.Zero;
            //IntPtr pcChannelCode = IntPtr.Zero;

            try
            {
                GET_URL_INFO_S getUrlInfo = new GET_URL_INFO_S();
                TIME_SLICE_S timeSlice = new TIME_SLICE_S();
                URL_INFO_S urlInfo = new URL_INFO_S();

                byte[] begin = new byte[IMOSSDK.IMOS_TIME_LEN];
                string strBeginTime = beginTime.ToString("yyyy-MM-dd HH:mm:ss");
                Encoding.UTF8.GetBytes(strBeginTime).CopyTo(begin, 0);
                byte[] end = new byte[IMOSSDK.IMOS_TIME_LEN];
                string strEndTime = endTime.ToString("yyyy-MM-dd HH:mm:ss");
                Encoding.UTF8.GetBytes(strEndTime).CopyTo(end, 0);

                timeSlice.szBeginTime = begin;
                timeSlice.szEndTime = end;

                getUrlInfo.szCamCode = camCode;
                getUrlInfo.szFileName = fileInfo.szFileName;
                getUrlInfo.stRecTimeSlice = timeSlice;
                getUrlInfo.szClientIp = stLoginInfo.stUserLoginIDInfo.szUserIpAddress;

                //ptrSDKURLInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(URL_INFO_S)));
                //这个下载通道号，是录像下载的唯一标志，以后查询录像都要用到这个通道号
                byte[] byPcChannel = new byte[IMOSSDK.IMOS_RES_CODE_LEN];

                ulRet = IMOSSDK.IMOS_GetRecordFileURL(ref stLoginInfo.stUserLoginIDInfo, ref getUrlInfo, ref urlInfo);
                if (ulRet != 0)
                {
                    Logger.Info(typeof(SdkManager), "IMOS_GetRecordFileURL 失败:" + ErrorCode.GetErrorMsg(ulRet));

                }

                ulRet = IMOSSDK.IMOS_OpenDownload(ref stLoginInfo.stUserLoginIDInfo,
                    urlInfo.szURL, urlInfo.stVodSeverIP.szServerIp, urlInfo.stVodSeverIP.usServerPort,
                    (uint)vodProtocol, (uint)speed,
                    Encoding.Default.GetBytes(downloadLoc), downloadFormat, byPcChannel);
                if (ulRet != 0)
                {
                    Logger.Info(typeof(SdkManager), "IMOS_OpenDownload 失败:" + ErrorCode.GetErrorMsg(ulRet));

                }

                ulRet = IMOSSDK.IMOS_SetDecoderTag(ref stLoginInfo.stUserLoginIDInfo, byPcChannel, urlInfo.szDecoderTag);
                if (ulRet != 0)
                {
                    Logger.Info(typeof(SdkManager), "IMOS_SetDecoderTag 失败:" + ErrorCode.GetErrorMsg(ulRet));

                }

                ulRet = IMOSSDK.IMOS_StartDownload(ref stLoginInfo.stUserLoginIDInfo, byPcChannel);
                if (ulRet != 0)
                {
                    Logger.Info(typeof(SdkManager), "IMOS_StartDownload 失败:" + ErrorCode.GetErrorMsg(ulRet));

                }

                return byPcChannel;
            }
            catch (System.Exception ex)
            {
                Logger.Info(typeof(SdkManager), ex);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(ptrSDKURLInfo);
            }
        }

        /// <summary>
        /// 获取当前下载时间
        /// </summary>
        /// <param name="channelCode">下载通道号</param>
        /// <returns></returns>
        public DateTime GetCurrDownLoadTime(byte[] channelCode)
        {
            dtFormat.ShortTimePattern = "yyyy-MM-dd HH:mm:ss";
            DateTime dt = new DateTime();
            uint ulRet = 0;

            byte[] currTime = new byte[IMOSSDK.IMOS_TIME_LEN];   //当前下载时间;
            ulRet = IMOSSDK.IMOS_GetDownloadTime(ref stLoginInfo.stUserLoginIDInfo, Encoding.UTF8.GetString(channelCode), currTime);
            if (ulRet != 0)
            {
                Logger.Info(typeof(SdkManager), "IMOS_GetDownloadTime 失败:" + ErrorCode.GetErrorMsg(ulRet));

            }

            string strTime = Encoding.UTF8.GetString(currTime);

            if (!String.IsNullOrEmpty(strTime.TrimEnd('\0')))
            {
                dt = Convert.ToDateTime(strTime, dtFormat);
            }
            return dt;
        }


        public uint RegCallBackFunc(USER_LOGIN_ID_INFO_S stLoginInfo)
        {
            uint ulRet = 0;
            //先订阅推送
            ulRet = IMOSSDK.IMOS_SubscribePushInfo(ref stLoginInfo, (uint)SUBSCRIBE_PUSH_TYPE_E.SUBSCRIBE_PUSH_TYPE_ALL);
            if (0 != ulRet)
            {
                Logger.Info(typeof(SdkManager), "IMOS_SubscribePushInfo 订阅警告失败:" + ErrorCode.GetErrorMsg(ulRet));
                return ulRet;
            }

            CallBackFunc = ProcessCallBack;
            IntPtr ptrCB = Marshal.GetFunctionPointerForDelegate(CallBackFunc);
            //注册回调
            ulRet = IMOSSDK.IMOS_RegCallBackPrcFunc(ref stLoginInfo, ptrCB);
            if (0 != ulRet)
            {
                Logger.Info(typeof(SdkManager), "IMOS_RegCallBackPrcFunc 注册回调函数出错:" + ErrorCode.GetErrorMsg(ulRet));
                return ulRet;
            }
            return ulRet;
        }



        //回调系统报警处理函数
        public void ProcessCallBack(uint ulProcType, IntPtr ptrParam)
        {

            try
            {
                switch (ulProcType)
                {
                    case (uint)CALL_BACK_PROC_TYPE_E.PROC_TYPE_LOGOUT:
                        //添加再次登录方法
                         LoginAgain();
                        break;
                 
                    default:
                        break;

                }
            }
            catch (SystemException ex)
            {
                Logger.Info(typeof(SdkManager), ex);
            }

        }

        public void LoginAgain()
        {
            MessageBox.Show("保活失败！");
            LogOut();

            //断线重连
           sdkManager.LoginMethod(this.usrLoginName, this.usrLoginPsw, this.srvIpAddr, "N/A");

        }

        //A8大屏控制
        public A8_INFO_S QueryA8(string a8CodeStr)
        {

            uint ulRet = 0;

            // string a8codestr = "DeviceCode";
            A8_INFO_S a8_Info_S;

            byte[] a8code = new byte[IMOSSDK.IMOS_CODE_LEN];
            Encoding.UTF8.GetBytes(a8CodeStr).CopyTo(a8code, 0);
            IntPtr ptrA8_Info_S = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(A8_INFO_S)));

            ulRet = IMOSSDK.IMOS_QueryA8(ref stLoginInfo.stUserLoginIDInfo, a8code, ptrA8_Info_S);
            if (0 != ulRet)
            {
               Logger.Info(typeof(SdkManager), "IMOS_QueryA8 查询A8失败:" + ErrorCode.GetErrorMsg(ulRet));

            }
            a8_Info_S = (A8_INFO_S)Marshal.PtrToStructure(ptrA8_Info_S, typeof(A8_INFO_S));
            return a8_Info_S;

        }



        public TV_WALL_WINDOWS_INFO OpenWindowA8(uint ulSplitType, uint ulLevel, byte[] szTVWallCode)
        {

            uint ulRet = 0;

            //构造TV_Wall结构体
            TV_WALL_WINDOWS_INFO tv_Wall_Windows = new TV_WALL_WINDOWS_INFO();

            //byte[] szTVWallCode = new byte[IMOSSDK.IMOS_CODE_LEN];
            //Encoding.UTF8.GetBytes("I").CopyTo(szTVWallCode, 0);

            //tv_Wall_Windows.szTVWallCode = szTVWallCode;
            tv_Wall_Windows.ulSplitType = ulSplitType;
            tv_Wall_Windows.ulLevel = ulLevel;

            AREA_SCOPE_S area_Scope_S = new AREA_SCOPE_S();
            area_Scope_S.ulTopLeftX = 10080 * 2;
            area_Scope_S.ulTopLeftY = 0;
            area_Scope_S.ulBottomRightX = 10080 * 3;
            area_Scope_S.ulBottomRightY = 10080;
            tv_Wall_Windows.stPosition = area_Scope_S;

            CAMERA_UNIT_INFO_S[] camera_unit_info_s = new CAMERA_UNIT_INFO_S[(int)SCREEN_SPLIT_TYPE_E.SCREEN_SPLIT_TYPE_SIXTEEN];
            for (int i = 0; i < 16; i++)
            {
                byte[] szCamName = new byte[IMOSSDK.IMOS_NAME_LEN];
                byte[] szCamCode = new byte[IMOSSDK.IMOS_CODE_LEN];
                Encoding.UTF8.GetBytes("INVALID_CAMERA_CODE").CopyTo(szCamCode, 0);
                camera_unit_info_s[i].szCamCode = szCamCode;
                camera_unit_info_s[i].szCamName = szCamName;

            }
            tv_Wall_Windows.stCamUnitInfo = camera_unit_info_s;
            tv_Wall_Windows.szTVWallCode = szTVWallCode;

            ulRet = IMOSSDK.IMOS_OpenWindow(ref stLoginInfo.stUserLoginIDInfo, ref tv_Wall_Windows);
            if (0 != ulRet)
            {
#if DEBUG
                MessageBox.Show("打开电视墙窗口失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Info(typeof(SdkManager), "IMOS_OpenWindow 打开电视墙窗口失败:" + ErrorCode.GetErrorMsg(ulRet));

            }
            return tv_Wall_Windows;

        }

        public TV_WALL_ALL_INFO_S IMOS_QueryTVWallA8(byte[] pcTVWallCode)
        {
            uint ulRet = 0;
            IntPtr stTVWall = new IntPtr();
            stTVWall = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TV_WALL_ALL_INFO_S)));
            var a = Marshal.SizeOf(typeof(TV_WALL_ALL_INFO_S));
            ulRet = IMOSSDK.IMOS_QueryTVWallA8(ref stLoginInfo.stUserLoginIDInfo, pcTVWallCode, stTVWall);

            if (0 != ulRet)
            {
#if DEBUG
                MessageBox.Show("IMOS_QueryTVWallA8 查询电视墙窗口失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Info(typeof(SdkManager), "IMOS_QueryTVWallA8 查询电视墙窗口失败:" + ErrorCode.GetErrorMsg(ulRet));

            }

            TV_WALL_ALL_INFO_S tvWallInfo = (TV_WALL_ALL_INFO_S)Marshal.PtrToStructure(stTVWall, typeof(TV_WALL_ALL_INFO_S));

            return tvWallInfo;


        }

        //查询TV_Wall_列表
        public List<TV_WALL_A8_INFO> QueryTVWallListA8()
        {

            IntPtr ptrRspPage = IntPtr.Zero;
            IntPtr pstTVWallList = IntPtr.Zero;
            uint ulRet = 0;

            try
            {
                uint ulBeginNum = 0;
                uint ulTotalNum = 0;

                ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));
                pstTVWallList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TV_WALL_A8_INFO)) * 30);
                COMMON_QUERY_CONDITION_S commonQueryConditions = new COMMON_QUERY_CONDITION_S();
                RSP_PAGE_INFO_S stRspPageInfo;
                List<TV_WALL_A8_INFO> list = new List<TV_WALL_A8_INFO>();
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 1;

                    ulRet = IMOSSDK.IMOS_QueryTVWallListA8(ref stLoginInfo.stUserLoginIDInfo, ref commonQueryConditions, ref stPageInfo, ptrRspPage, pstTVWallList);

                    if (0 != ulRet)
                    {
#if DEBUG
                        MessageBox.Show("IMOS_QueryTVWallListA8 查询电视墙列表失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                        Logger.Info(typeof(SdkManager), "IMOS_QueryTVWallListA8 查询电视墙列表失败:" + ErrorCode.GetErrorMsg(ulRet));

                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    TV_WALL_A8_INFO stTVWallA8Info;
                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(pstTVWallList.ToInt64() + Marshal.SizeOf(typeof(TV_WALL_A8_INFO)) * i);
                        stTVWallA8Info = (TV_WALL_A8_INFO)Marshal.PtrToStructure(ptrTemp, typeof(TV_WALL_A8_INFO));
                        list.Add(stTVWallA8Info);
                        string tvWallCode = Encoding.Default.GetString(stTVWallA8Info.stTVWallBaseInfo.szTVWallCode);


                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;

            }
            catch (Exception ex)
            {
                //log.Info(ex.StackTrace);
                Logger.Info(typeof(SdkManager), ex);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(pstTVWallList);
                Marshal.FreeHGlobal(ptrRspPage);
            }
        }

        public void CloseWindowA8(byte[] pcWindowCode)
        {
            uint ulRet = 0;
            ulRet = IMOSSDK.IMOS_CloseWindow(ref stLoginInfo.stUserLoginIDInfo, pcWindowCode);

            if (0 != ulRet)
            {
#if DEBUG
                MessageBox.Show("IMOS_CloseWindow 关闭电视墙窗口失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Info(typeof(SdkManager), "IMOS_CloseWindow 关闭电视墙窗口失败:" + ErrorCode.GetErrorMsg(ulRet));

            }

        }

        /*
          IMOS_ModWindow修改窗口信息接口，用于（包括移动，放大缩小，分屏，修改单个窗口层次，修改窗口内实况信息等操作），
          构造一个TV_WALL_WINDOWS_INFO
          （1）移动，放大缩小操作：修改窗口内坐标信息AREA_SCOPE_S，bSingleDB为0，其他参数为开窗时保留的参数信息（包括窗口编码，电视墙编码等）
          （2）分屏操作：修改窗口内ulSplitType值为对应分屏数，bSingleDB为0，其他参数为开窗时保留的参数信息（包括窗口编码，电视墙编码等）
          （3）修改单个窗口层次：修改窗口内层次数ulLevel（当前电视墙内所有窗口层次的最大值+1），与电视墙内其他窗口层次不能重复，用于窗口置顶操作，bSingleDB为     0，其他参数为开窗时保留的参数信息（包括窗口编码，电视墙编码等）
          （4）修改窗口内实况信息：修改参数为bSingleDB为1，stCamUnitInfo[分屏号-1]内的szCamCode，开启实况成功时调用将szCamCode修改为摄像机编码，停止实况成功时调用将szCamCode修改为INVALID_CAMERA_CODE，其他参数为开窗时保留的参数信息（包括窗口编码，电视墙编码等）
        */
        public void ModifyWindowA8(TV_WALL_WINDOWS_INFO pstWindowInfo, bool bSingleDB)
        {
            uint ulRet = 0;
            ulRet = IMOSSDK.IMOS_ModWindow(ref stLoginInfo.stUserLoginIDInfo, ref pstWindowInfo, bSingleDB);

            if (0 != ulRet)
            {

#if DEBUG
                MessageBox.Show("IMOS_ModWindow 修改电视墙窗口失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Info(typeof(SdkManager), "IMOS_ModWindow 修改电视墙窗口失败:" + ErrorCode.GetErrorMsg(ulRet));
            }
        }

        public void SetWindowLevel(TV_WALL_WINDOWS_INFO pstWindowInfo, uint level)
        {
            uint ulRet = 0;
            ulRet = IMOSSDK.IMOS_SetWindowLevel(ref stLoginInfo.stUserLoginIDInfo, ref pstWindowInfo, level);

            if (0 != ulRet)
            {

#if DEBUG
                MessageBox.Show("IMOS_SetWindowLevel 修改窗口层级失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Info(typeof(SdkManager), "IMOS_SetWindowLevel 修改窗口层级失败:" + ErrorCode.GetErrorMsg(ulRet));
            }
        }
        public void StartA8Monitor(byte[] cameraCode, byte[] monitorCode)
        {
            uint ulRet = 0;
            ulRet = IMOSSDK.IMOS_StartA8Monitor(ref stLoginInfo.stUserLoginIDInfo, cameraCode, monitorCode, 1, 0);

            if (0 != ulRet)
            {

#if DEBUG
                MessageBox.Show("IMOS_StartA8Monitor 开始A8推大屏失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Info(typeof(SdkManager), "IMOS_StartA8Monitor 开始A8推大屏失败:" + ErrorCode.GetErrorMsg(ulRet));
            }


        }

        public void StopA8Monitor(byte[] monitorCode)
        {
            uint ulRet = 0;
            ulRet = IMOSSDK.IMOS_StopA8Monitor(ref stLoginInfo.stUserLoginIDInfo, monitorCode, (uint)CS_OPERATE_CODE_E.USER_OPERATE_SERVICE);

            if (0 != ulRet)
            {

#if DEBUG
                MessageBox.Show("IMOS_StopA8Monitor 停止A8推大屏失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Info(typeof(SdkManager), "IMOS_StopA8Monitor 停止A8推大屏失败:" + ErrorCode.GetErrorMsg(ulRet));
            }
        }

        public List<TV_WALL_BASE_INFO_A8_S> IMOS_QueryTVWallSceneListA8(byte[] pcTVWallCode, uint pulSceneNum)
        {
            uint ulRet = 0;

            IntPtr pstTVWallSceneList = IntPtr.Zero;

            pstTVWallSceneList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TV_WALL_BASE_INFO_A8_S)) * 30);
            List<TV_WALL_BASE_INFO_A8_S> list = new List<TV_WALL_BASE_INFO_A8_S>();

            ulRet = IMOSSDK.IMOS_QueryTVWallSceneListA8(ref stLoginInfo.stUserLoginIDInfo, pcTVWallCode, pulSceneNum, pstTVWallSceneList);

            if (0 != ulRet)
            {

#if DEBUG
                MessageBox.Show("IMOS_QueryTVWallSceneListA8 查询大屏场景列表失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Info(typeof(SdkManager), "IMOS_QueryTVWallSceneListA8 查询大屏场景列表失败:" + ErrorCode.GetErrorMsg(ulRet));
            }

            TV_WALL_BASE_INFO_A8_S stTVWallBaseInfoA8;
            for (int i = 0; i < 30; ++i)
            {
                IntPtr ptrTemp = new IntPtr(pstTVWallSceneList.ToInt64() + Marshal.SizeOf(typeof(TV_WALL_BASE_INFO_A8_S)) * i);
                stTVWallBaseInfoA8 = (TV_WALL_BASE_INFO_A8_S)Marshal.PtrToStructure(ptrTemp, typeof(TV_WALL_BASE_INFO_A8_S));
                list.Add(stTVWallBaseInfoA8);
            }
            return list;
        }

        public void IMOS_TVWallSceneEnable(byte[] pcTVWallCode, uint ulSceneID)
        {
            uint ulRet = 0;
            ulRet = IMOSSDK.IMOS_TVWallSceneEnable(ref stLoginInfo.stUserLoginIDInfo, pcTVWallCode, ulSceneID);

            if (0 != ulRet)
            {
                MessageBox.Show("设置大屏场景失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#if DEBUG
                MessageBox.Show("IMOS_TVWallSceneEnable 设置大屏场景失败！ 错误代码：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Info(typeof(SdkManager), "IMOS_TVWallSceneEnable 设置大屏场景失败:" + ErrorCode.GetErrorMsg(ulRet));
            }

        }

        public void StartMonitorA8()
        {
            byte[] szTVWallCode;

            //查询TVWALL代码
            List<TV_WALL_A8_INFO> tvWallList = sdkManager.QueryTVWallListA8();

            szTVWallCode = tvWallList[0].stTVWallBaseInfo.szTVWallCode;

            var tvWallA8 = sdkManager.IMOS_QueryTVWallA8(szTVWallCode);
            uint level = 0;
            for (int i = 0; i < tvWallA8.ulWindowsNum; i++)
            {
                if (level < tvWallA8.stMosaicScreenInfo[i].ulLevel)
                    level = tvWallA8.stMosaicScreenInfo[i].ulLevel;
            }

            uint splitType = 1;
            var windowA8 = sdkManager.OpenWindowA8(splitType, ++level, szTVWallCode);
            string windowCode = Encoding.Default.GetString(windowA8.szWindowCode);
            string monitorCode = windowCode.Trim('\0') + "!1";
            string camerCode = "DeviceCode-cam-0001";

            monitorCode = monitorCode.PadRight(48, '\0');
            camerCode = camerCode.PadRight(48, '\0');
            sdkManager.StartA8Monitor(Encoding.Default.GetBytes(camerCode), Encoding.Default.GetBytes(monitorCode));

            sdkManager.ModifyWindowA8(windowA8, false);
            windowA8.stCamUnitInfo[0].ulStatus = 1;
            sdkManager.ModifyWindowA8(windowA8, false);
            tvWallA8 = sdkManager.IMOS_QueryTVWallA8(szTVWallCode);

            sdkManager.StopA8Monitor(Encoding.Default.GetBytes(monitorCode));
            sdkManager.CloseWindowA8(windowA8.szWindowCode);
        }

        public static uint GetEcInfoByCameraCode(string cameraCode, ref EC_INFO_S stEcInfo, USER_LOGIN_ID_INFO_S stLoginInfo)
        {
            CAMERA_INFO_S cAMERA_INFO_S = default(CAMERA_INFO_S);
            uint num = IMOSSDK.IMOS_QueryCamera(ref stLoginInfo, cameraCode, ref cAMERA_INFO_S);
            if (num == 0u)
            {
                num = IMOSSDK.IMOS_QueryEcInfo(ref stLoginInfo, cAMERA_INFO_S.szECCode, ref stEcInfo);
                if (num != 0u)
                {
                   Logger.Error(typeof(SdkManager), "IMOS_QueryEcInfo fail errcode:" + num.ToString());
                }
            }
            else
            {
               Logger.Error(typeof(SdkManager), "IMOS_QueryCamera fail errcode:" + num.ToString());
            }
            return num;
        }
        public static List<ORG_RES_QUERY_ITEM_S> QueryResourceListV2(byte[] byResourceCode, IMOS_TYPE_E enImosType)
        {
            int num = Marshal.SizeOf(typeof(RES_ITEM_V2_S));
            List<ORG_RES_QUERY_ITEM_S> list = new List<ORG_RES_QUERY_ITEM_S>();
            IntPtr intPtr = IntPtr.Zero;
            try
            {
                COMMON_QUERY_CONDITION_S cOMMON_QUERY_CONDITION_S = default(COMMON_QUERY_CONDITION_S);
                cOMMON_QUERY_CONDITION_S.astQueryConditionList = new QUERY_CONDITION_ITEM_S[16];
                QUERY_CONDITION_ITEM_S qUERY_CONDITION_ITEM_S = default(QUERY_CONDITION_ITEM_S);
                qUERY_CONDITION_ITEM_S.ulQueryType = 0u;
                qUERY_CONDITION_ITEM_S.ulLogicFlag = 0u;
                qUERY_CONDITION_ITEM_S.szQueryData = new byte[64];
                byResourceCode.CopyTo(qUERY_CONDITION_ITEM_S.szQueryData, 0);
                QUERY_CONDITION_ITEM_S qUERY_CONDITION_ITEM_S2 = default(QUERY_CONDITION_ITEM_S);
                qUERY_CONDITION_ITEM_S2.ulQueryType = 11u;
                qUERY_CONDITION_ITEM_S2.ulLogicFlag = 6u;
                QUERY_CONDITION_ITEM_S qUERY_CONDITION_ITEM_S3 = default(QUERY_CONDITION_ITEM_S);
                qUERY_CONDITION_ITEM_S3.ulQueryType = 1u;
                qUERY_CONDITION_ITEM_S3.ulLogicFlag = 6u;
                if (IMOS_TYPE_E.IMOS_TYPE_ORG == enImosType)
                {
                    cOMMON_QUERY_CONDITION_S.ulItemNum = 3u;
                    cOMMON_QUERY_CONDITION_S.astQueryConditionList[0] = qUERY_CONDITION_ITEM_S;
                    cOMMON_QUERY_CONDITION_S.astQueryConditionList[1] = qUERY_CONDITION_ITEM_S2;
                    cOMMON_QUERY_CONDITION_S.astQueryConditionList[2] = qUERY_CONDITION_ITEM_S3;
                }
                else
                {
                    cOMMON_QUERY_CONDITION_S.ulItemNum = 2u;
                    cOMMON_QUERY_CONDITION_S.astQueryConditionList[0] = qUERY_CONDITION_ITEM_S;
                    cOMMON_QUERY_CONDITION_S.astQueryConditionList[1] = qUERY_CONDITION_ITEM_S3;
                }
                QUERY_PAGE_INFO_S qUERY_PAGE_INFO_S = default(QUERY_PAGE_INFO_S);
                qUERY_PAGE_INFO_S.ulPageFirstRowNumber = 0u;
                qUERY_PAGE_INFO_S.ulPageRowNum = 200u;
                RSP_PAGE_INFO_S rSP_PAGE_INFO_S = default(RSP_PAGE_INFO_S);
                intPtr = Marshal.AllocHGlobal(num * 200);
                LOGIN_INFO_S loginInfo = SdkManager.GetInstance().stLoginInfo;
                uint num2;
                while (true)
                {
                    rSP_PAGE_INFO_S.ulRowNum = 0u;

                    num2 = IMOSSDK.IMOS_QueryResourceListV2(ref loginInfo.stUserLoginIDInfo, "iccsid", ref cOMMON_QUERY_CONDITION_S, ref qUERY_PAGE_INFO_S, ref rSP_PAGE_INFO_S, intPtr);
                    if (num2 != 0u)
                    {
                        break;
                    }
                    int num3 = 0;
                    while ((long)num3 < (long)((ulong)rSP_PAGE_INFO_S.ulRowNum))
                    {
                        IntPtr ptr = (IntPtr)(intPtr.ToInt64() + num * num3);
                        // RES_ITEM_V2_S rES_ITEM_V2_S = default(RES_ITEM_V2_S);
                        list.Add(((RES_ITEM_V2_S)Marshal.PtrToStructure(ptr, typeof(RES_ITEM_V2_S))).stResItemV1);
                        num3++;
                    }
                    qUERY_PAGE_INFO_S.ulPageFirstRowNumber += rSP_PAGE_INFO_S.ulRowNum;
                    if (rSP_PAGE_INFO_S.ulRowNum != qUERY_PAGE_INFO_S.ulPageRowNum)
                    {
                        goto Block_7;
                    }
                }
                Logger.Error(typeof(SdkManager), "IMOS_QueryResourceListV2 fail errcode:" + num2.ToString());
                return list;
                Block_7:;
            }
            catch (Exception err)
            {
                Logger.Error(typeof(SdkManager), "QueryCamera exception" + err);
            }
            finally
            {
                Marshal.FreeHGlobal(intPtr);
            }
            return list;
        }
        public static ResCameInfo GetCameraInfobyCode(string camereCode)
        {
            ResCameInfo result;
            try
            {
                int num = 0;
                List<ORG_RES_QUERY_ITEM_S> list = QueryResourceListV2(Encoding.Default.GetBytes(camereCode), IMOS_TYPE_E.IMOS_TYPE_CAMERA);
                if ((long)num != 0L)
                {
                    Logger.Error(typeof(SdkManager), "GetCameraInfobyCode failed:resCode " + num.ToString());
                    result = null;
                }
                else if (list == null || list.Count <= 0)
                {
                    Logger.Error(typeof(SdkManager), "GetCameraInfobyCode failed, Reuource is null");
                    result = null;
                }
                else
                {
                    ResCameInfo resCameInfo = new ResCameInfo
                    {
                        ResCode = Encoding.Default.GetString(list.FirstOrDefault().szResCode),
                        ResName = Encoding.Default.GetString(list.FirstOrDefault().szResName),
                        ResSubType = list.FirstOrDefault().ulResSubType,
                        ResType = list.FirstOrDefault().ulResType,
                        ResStatus = list.FirstOrDefault().ulResStatus
                    };
                    result = resCameInfo;
                }
            }
            catch (Exception err)
            {
                Logger.Error(typeof(SdkManager), err);
                result = null;
            }
            return result;
        }
        //Todo: change to sdk method
        public bool IsCameraOnline(string cameracode)
        // => CameraList.Select(itemS => { Encoding.Default.GetString(itemS.szResCode).TrimEnd('\0'); }).Any(code => code == cameracode);
        {
            foreach(var cam in CameraList)
            {
                if (cam.ulResStatus == IMOSSDK.IMOS_DEV_STATUS_ONLINE && Encoding.Default.GetString(cam.szResCode).TrimEnd('\0')==cameracode)
                {
                    return true;
                } 
            }
            return false;
         
        }

        public uint CameraType(string cameracode)
        {

            List<ORG_RES_QUERY_ITEM_S> ret= CameraList.Where(x=> Encoding.Default.GetString(x.szResCode).TrimEnd('\0')== cameracode).ToList();
            if (ret.Count !=0)
            {
                return ret[0].ulResSubType;
            }
            //foreach (var cam in CameraList)
            //{
            //    string code = Encoding.Default.GetString(cam.szResCode).TrimEnd('\0');
            //    if ( Encoding.Default.GetString(cam.szResCode).TrimEnd('\0') == cameracode)
            //    {
            //      return  cam.ulResSubType;
            //    }
            //}
            return 0;

        }
    }
    public class ResCameInfo
    {
        public string ResCode = string.Empty;
        public string ResName = string.Empty;
        public uint ResType;
        public uint ResSubType;
        public uint ResStatus;
    }
}

