using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

using System.Reflection;
namespace ManagementProject
{
    public enum BAK_RES_STATUS_E : uint
    {
        BAK_RES_UNFORMATTED,
        BAK_RES_FORMATTING,
        BAK_RES_ONLINE,
        BAK_RES_OFFLINE,
        BAK_RES_STATUS_MAX,
        BAK_RES_STATUS_INVALID = 4294967295u
    }

    public enum CAMERA_TYPE_E : uint
    {
        [Description("����̶������")]
        CAMERA_TYPE_FIX = 1,            /**< ����̶������ */
        [Description("����̶������")]
        CAMERA_TYPE_PTZ = 2,            /**< ����̶������ */
        [Description("����̶������")]
        CAMERA_HD_TYPE_FIX = 3,            /**< ����̶������ */
        [Description("������̨�����")]
        CAMERA_HD_TYPE_PTZ = 4,            /**< ������̨����� */
        [Description("���������")]
        CAMERA_TYPE_CAR = 5,            /**< ��������� */
        [Description("���������")]
        CAMERA_TYPE_VIRTUAL = 6,            /**< ��������� */
        [Description("���ɿر������")]
        CAMERA_TYPE_BALL_NOT_CONTROL = 7,            /**< ���ɿر������ */
        [Description("���ɿر������")]
        CAMERA_HD_TYPE_BALL_NOT_CONTROL = 8,            /**< ���ɿر������ */

        /* BEGIN: Added by kf0092 for ��ȫ��������, 2013��3��22�� */
        [Description("VM��ȫ���������")]
        CAMERA_TYPE_SAFE_VM = 9,            /**< VM��ȫ��������� */
        [Description("DVR��ȫ���������")]
        CAMERA_TYPE_SAFE_DVR = 10,           /**< DVR��ȫ��������� */
        [Description("����ȫ���������")]
        CAMERA_TYPE_SAFE_MATRIX = 11,           /**< ����ȫ��������� */

        /* END  : Added by kf0092 for ��ȫ��������, 2013��3��22�� */
        [Description("�佹ǹ��")]
        CAMERA_TYPE_FIX_BOX = 12,                /**<�佹ǹ�� */

        /* Begin added by y01359, 2014-12-11 for A8 */
        [Description("VGA���������")]
        CAMERA_TYPE_VGA = 13,                   /**< VGA��������� */
        /* End added by y01359, 2014-12-11 for A8 */
        [Description("���������ö�����ֵ")]
        CAMERA_TYPE_MAX,                        /**< ���������ö�����ֵ */
        [Description("��Чֵ")]
        CAMERA_TYPE_INVALID = 0xFFFFFFFF    /**< ��Чֵ */
    }
    public enum CS_OPERATE_CODE_E
    {
        USER_OPERATE_SERVICE = 0,                               /**< �û������������ */

        /*****************��������������Ķ�������(��ʼ)*******************/

        SWITCH_OPERATE = 1,    /**< ���в��� */
        PLAN_SWITCH_OPERATE = 2,    /**< �ƻ����в��� */
        SERVICE_REAVE = 3,    /**< ҵ����ռ */
        ALARM_LINKAGE_START_SERVICE = 4,    /**< �澯���� */
        EXT_DOMAIN_OPER_SERVICE = 5,    /**< ��������� */

        EC_ONLINE = 6,    /**< EC���� */
        EC_OFFLINE = 7,    /**< EC���� */
        EC_DELETE = 8,    /**< ECɾ�� */
        CAMERA_ONLINE = 9,    /**< ��������� */
        CAMERA_OFFLINE = 10,   /**< ��������� */

        DC_ONLINE = 11,   /**< DC���� */
        DC_OFFLINE = 12,   /**< DC���� */
        DC_DELETE = 13,   /**< DCɾ�� */

        MS_ONLINE = 14,   /**< MS���� */
        MS_OFFLINE = 15,   /**< MS���� */
        MS_OFFLINE_TRANSFER = 16,   /**< MS���������ת�� */
        MS_DELETE_TRANSFER = 17,   /**< MSɾ�������ת�� */
        MS_DELETE = 18,   /**< MSɾ�� */

        VX500_ONLINE = 19,   /**< VX500���� */
        VX500_OFFLINE = 20,   /**< VX500���� */
        VX500_DELETE = 21,   /**< VX500ɾ�� */

        ISC_ONLINE = 22,   /**< ISC3000-E���� */
        ISC_OFFLINE = 23,   /**< ISC3000-E���� */

        EX_DOMAIN_JUNIOR_ONLINE = 24,   /**< �¼������� */
        EX_DOMAIN_SUPERIOR_ONLINE = 25,   /**< �ϼ������� */
        EX_DOMAIN_JUNIOR_OFFLINE = 26,   /**< �¼������� */
        EX_DOMAIN_SUPERIOR_OFFLINE = 27,   /**< �ϼ������� */
        EXT_DOMAIN_DELETE = 28,   /**< ����ɾ�� */

        EXT_DOM_CANCEL_SHR_CAM = 29,   /**< ����ȡ����������������� */
        LOC_DOM_CANCEL_SHR_CAM = 30,   /**< ����ȡ����������������� */

        CAMERA_DELETE = 31,   /**< CAMERAɾ�� */
        MONITOR_DELETE = 32,   /**< MONITORɾ�� */

        SWITCH_RES_MDF = 33,   /**< ������Դ�޸� */
        SWITCH_RES_DEL = 34,   /**< ������Դɾ�� */
        SWITCH_PLAN_DEL = 35,   /**< ���мƻ�ɾ�� */
        SWITCH_PLAN_MODIFY = 36,   /**< ���мƻ��޸� */

        XP_ONLINE = 37,   /**< XP���� */
        XP_OFFLINE = 38,   /**< XP���� */
        USER_KEEPALIVE_FAIL = 39,   /**< �û�����ʧ�� */
        USER_DELETE = 40,   /**< �û���ɾ�� */
        USER_QUIT = 41,   /**< �û��˳� */
        USER_KICKED_OUT = 42,   /**< �û���ǿ������ */

        SYSMANGER_OPERATE_SERVICE = 43,   /**< ����Ա���� */
        PTZ_LINK_TIMER_OUT = 44,   /**< ��̨���ӳ�ʱ */
        PTZ_FIRST_LINK = 45,   /**< ��̨�״����� */
        PTZ_LOW_AUTHORITY = 46,   /**< ��̨����Ȩ�޵� */
        PTZ_NO_AUTHORITY = 47,   /**< ����̨����Ȩ�� */
        PTZ_HAS_LOCKED = 48,   /**< ��̨������ */

        DEV_MEDIA_PARAM_CHANGE = 49,   /**< �豸ý������޸� */

        SALVO_OPERATE = 50,   /**< ����ʾ���� */

        SALVO_RES_DEL = 51,   /**< ����ʾɾ�� */
        SALVO_RES_MDF = 52,   /**< ����ʾ�޸� */

        GROUPSWITCH_RES_DEL = 53,   /**< ����Ѳ��Դɾ�� */
        GROUPSWITCH_RES_MDF = 54,   /**< ����Ѳ��Դ�޸� */

        GROUPSWITCH_PLAN_DEL = 55,   /**< ����Ѳ�ƻ�ɾ�� */
        GROUPSWITCH_PLAN_MODIFY = 56,   /**< ����Ѳ�ƻ��޸� */

        GROUPSALVO_OPERATE = 57,   /**< ����Ѳ���� */
        GROUPSALVO_STOP = 58,   /**< ����Ѳֹͣ */
        GROUPSALVO_PLAN_OPERATE = 59,   /**< ����Ѳ�ƻ����� */

        INTERNAL_ERR_OPERATE = 60,   /**< �ڲ������������ */

        PTZ_CCB_FULL = 61,   /**< ���ƿ����� */

        MONITOR_SPLIT_SCREEN_DELETE = 62,   /**< ����������ɾ�� */
        MONITOR_SPLIT_SCREEN_SWITCH = 63, /**< �����������л� */

        ALARM_LINKAGE_RESUME_SERVICE = 64,  /**< �澯�����ָ� */
        MONITOR_SPLIT_SCREEN_SWITCH_STOP = 65, /**< �����������л������"ֹͣҵ��"���� */
        MONITOR_SPLIT_SCREEN_SWITCH_START = 66, /**< �����������л������"����ҵ��"���� */
        MONITOR_SPLIT_SCREEN_SWITCH_START_FULL = 67, /**< �����������л������"����ҵ��"���� - ����ȫ�� */
        MONITOR_SPLIT_SCREEN_SWITCH_START_EXIT = 68, /**< �����������л������"����ҵ��"���� - �˳�ȫ�� */

        CALLEE_USER_OPERATE_SERVICE = 70,   /**< �����û�������� */
        SYSTEM_OPERATE_SERVICE = 71,   /**< ϵͳ������� */
        CALLEE_NOT_SUPPORT_SERVICE = 72,   /**< ���в�֧�ִ�ҵ����� */

        EXDOMAIN_CRUISE_PATH_DELETE = 73,   /**< ����Ѳ��·��ɾ�� */

        TS_OFFLINE = 74,   /**< TS���� */
        TS_OFFLINE_TRANSFER = 75,   /**< TS���������ת�� */
        TS_DELETE_TRANSFER = 76,   /**< TSɾ�������ת�� */
        TS_DELETE = 77,   /**< TSɾ�� */
        TS_ONLINE = 78,   /**< TS���� */
        VOD_OFFLINEORDELETE = 79,   /**< VOD���������߻�ɾ�� */
        /* Begin Added by dengshuhua00673, 2012-11-27 of �Զ�������Ѳ */
        AUTOSALVO_OPERATE = 80,   /**< �Զ���������Ѳ���� */
        AUTOSALVO_STOP = 81,   /**< �Զ���������Ѳֹͣ */
        AUTOSWITCH_RES_MDF = 82,   /**< �Զ���������Ѳ��Դ�޸� */
        AUTOSWITCH_RES_DEL = 83,   /**< �Զ���������Ѳ��Դɾ�� */
        /* End Added by dengshuhua00673, 2012-11-27 of �Զ�������Ѳ */

        DC_CONFIGURE_DC_INST = 84,   /**< ����DCƴ�ӡ�ģ���������Ϣ */

        /* Begin: added by zkf0134, 2013.11.23 for �����л�֧������������Ӧ*/
        MONITOR_SWITCH_SPLIT_SCREEN_START = 85,   /**< �����������л������"����ҵ�����"*/
        /* End: added by zkf0134, 2013.11.23 for �����л�֧������������Ӧ*/

        PLAN_GUARD_OPERATE = 86,/**<�ƻ����ز���*/
        LS_OFFLINE = 87,   /**< LS���� */
        LS_OFFLINE_TRANSFER = 88,   /**< LS���������ת�� */
        LS_DELETE_TRANSFER = 89,   /**<LSɾ�������ת�� */
        LS_DELETE = 90,   /**< LSɾ�� */
        LS_ONLINE = 91,   /**< LS���� */

        VOD_ONLINE = 92,   /**< VOD���� */
        VOD_OFFLINE = 93,   /**< VOD���� */
        VOD_OFFLINE_TRANSFER = 94,   /**< VOD���������ת�� */
        VOD_DELETE_TRANSFER = 95,   /**< VODɾ�������ת�� */
        VOD_DELETE = 96,   /**<VODɾ�� */

        TS_RULE_CHANGE = 97,   /**< TS����ı� */
        /* Begin: added by y01359, 2015-01-28 for A8 */
        A8_ONLINE = 98,  /**< A8���� */
        A8_OFFLINE = 99,  /**< A8���� */
        A8_DELETE = 100,  /**< A8ɾ�� */
        A8_WINDOW_DELETE = 101,  /**< A8����ɾ�� */
        /* End: added by y01359, 2015-01-28 for A8 */

        /*****************��������������Ķ�������(����)*******************/

        CS_OPERATE_CODE_MAX,
        CS_OPERATE_CODE_INVALID = 0xFF
    }

    public enum AlARM_TYPE_E
    {
        /** MIB�澯 1~200 */
        AlARM_TYPE_HIGH_TEMPERATURE = 1,    /**< ���¸澯 */
        AlARM_TYPE_LOW_TEMPERATURE = 2,    /**< ���¸澯 */
        AlARM_TYPE_TEMPERATURE_RESUME = 3,    /**< �¶ȸ澯�ָ� */
        AlARM_TYPE_FAN_FAULT = 4,    /**< ���ȹ��ϸ澯 */
        AlARM_TYPE_FAN_FAULT_RESUME = 5,    /**< ���ȹ��ϸ澯�ָ� */
        ALARM_TYPE_FLASH_OPERATE = 6,    /**< Flash�����澯 */
        ALARM_TYPE_CPU_EXCEED = 7,    /**< CPU�����ʹ��߸澯 */
        ALARM_TYPE_MEMOY_EXCEED = 8,    /**< �ڴ������ʹ��߸澯 */
        ALARM_TYPE_REBOOT = 9,    /**< �豸���� */
        ALARM_TYPE_DEVICE_CONFIG_CHANGE = 10,   /**< �豸���ñ�� */
        AlARM_TYPE_DISK_ERR = 11,   /**< ���̹��� */
        AlARM_TYPE_DISK_ERR_RECOVER = 12,   /**< ���̹��ϻָ� */
        AlARM_TYPE_DISK_POWER_ON = 13,   /**< �������� */
        AlARM_TYPE_DISK_POWER_OFF = 14,   /**< �������� */
        AlARM_TYPE_RAID_EXCEPTION = 15,   /**< �����쳣�澯 */
        AlARM_TYPE_RAID_REBUILD_START = 16,   /**< �����ؽ�״̬ */
        AlARM_TYPE_RAID_REBUILD_FINISH = 17,   /**< �˳��ؽ� */
        AlARM_TYPE_STP_EXEC_EXCEPTION = 18,   /**< δ���ƻ�¼��澯 */
        AlARM_TYPE_STP_EXEC_RECOVER = 19,   /**< δ���ƻ�¼��ָ��澯 */
        AlARM_TYPE_IPSAN_NO_ACCESS = 20,   /**< �޷�����IPSAN�洢�豸�澯 */
        AlARM_TYPE_IPSAN_NO_ACCESS_RECOVER = 21,   /**< �޷�����IPSAN�洢�豸�ָ��澯 */
        AlARM_TYPE_STORED_DATA_READ_ERR = 22,   /**< ��ȡ�洢����ʧ�� */
        AlARM_TYPE_STORED_DATA_SEEK_ERR = 23,   /**< ��λ�洢�豸ʧ�� */
        AlARM_TYPE_VOD_OVER_THRESHOLD = 24,   /**< �㲥·��������ֵ */
        AlARM_TYPE_VOD_UNDER_THRESHOLD = 25,   /**< �㲥�������ָ� */
        ALARM_TYPE_TEMPERATURE = 26,   /**< �¶ȸ澯 */
        AlARM_TYPE_CAM_BAK_OWN_UNDER_THR = 27,   /**< �������������Դ����ʹ�ôﵽ��ֵ�ָ� */
        AlARM_TYPE_CAM_BAK_SHARD_OVER_THR = 28,   /**< �������������Դ����ʹ�ôﵽ��ֵ */
        AlARM_TYPE_CAM_BAK_SHARD_UNDER_THR = 29,   /**< �������������Դ����ʹ�ôﵽ��ֵ�ָ� */
        AlARM_TYPE_CAM_BAK_OWN_CAP_SCANT = 30,   /**< �������������Դ������ֹͣ����ʱ����Դʣ���������� */
        AlARM_TYPE_CAM_BAK_OWN_CAP_ENOUGH = 31,   /**< �������������Դ������ֹͣ����ʱ����Դʣ����������ָ� */
        AlARM_TYPE_BAK_RES_CAP_SCANT = 32,   /**< ȫ�ֱ���������ֹͣ����ʱ��ȫ����Դʣ���������� */
        AlARM_TYPE_BAK_RES_CAP_ENOUGH = 33,   /**< ȫ�ֱ���������ֹͣ����ʱ��ȫ����Դʣ����������ָ� */
        AlARM_TYPE_BAK_RES_ABNORMAL = 34,   /**< ������Դ�쳣 */
        AlARM_TYPE_BAK_RES_NORMAL = 35,   /**< ������Դ�쳣�ָ� */
        AlARM_TYPE_BAK_FAILED = 36,   /**< ��������ִ��ʧ�� */
        AlARM_TYPE_CAM_BAK_OWN_OVER_THR = 37,   /**< �������������Դ����ʹ�ôﵽ��ֵ */
        /* Begin: Added by mozhanfei(kf0149), 2013-9-9 for �����쳣�ָ��澯 */
        AlARM_TYPE_RAID_EXCEPTION_RECOVER = 38,   /**< �����쳣�ָ��澯 */
        /* End: Added by mozhanfei(kf0149), 2013-7-18 for �����쳣�ָ��澯 */

        AlARM_TYPE_PREVENT_REMOVAL = 71,   /**< ����澯 */
        AlARM_TYPE_PREVENT_REMOVAL_RESUME = 72,   /**< ����澯�ָ� */

        /* Begin added by baoyihui02795, 2011-04-28 of ������Ŀ */
        ALARM_TYPE_FLASHLIGHT_FAULT = 194,   /**< ����ƹ��� */
        ALARM_TYPE_FLASHLIGHT_FAULT_RESUME = 195,   /**< ����ƹ��ϻָ� */
        ALARM_TYPE_STOR_RES_ABNORMAL = 196,   /**< �洢��Դ�쳣 */
        ALARM_TYPE_STOR_RES_NORMAL = 197,   /**< �洢��Դ�쳣�ָ� */
        ALARM_TYPE_COIL_DISABLED = 198,   /**< ��ȦʧЧ */
        ALARM_TYPE_COIL_ENABLED = 199,   /**< ��ȦʧЧ�ָ� */
        /* End added by baoyihui02795, 2011-04-28 of ������Ŀ */

        /** SIP�澯 201~ */
        AlARM_TYPE_VIDEO_LOST = 201            ,  /**< ��Ƶ��ʧ�澯 */
        AlARM_TYPE_VIDEO_LOST_RESUME = 202     ,  /**< ��Ƶ��ʧ�澯�ָ� */
        [Description("�˶����澯")]
        AlARM_TYPE_MOVE_DETECT = 203           ,  /**< �˶����澯 */
        [Description("�˶����澯�ָ�")]
        AlARM_TYPE_MOVE_DETECT_RESUME = 204    ,  /**< �˶����澯�ָ� */
        [Description("�ڵ����澯")]
        AlARM_TYPE_MASK_DETECT = 205           ,  /**< �ڵ����澯 */
        [Description("�ڵ����澯�ָ�")]
        AlARM_TYPE_MASK_DETECT_RESUME = 206    ,  /**< �ڵ����澯�ָ� */
        AlARM_TYPE_INPUT_SWITCH = 207          ,  /**< ���뿪�����澯 */
        AlARM_TYPE_INPUT_SWITCH_RESUME = 208   ,  /**< ���뿪�����澯�ָ� */
        AlARM_TYPE_SHORT_CIRCUIT = 209         ,  /**< ��������·��·�澯 */
        AlARM_TYPE_BREAKER_CIRCUIT = 210       ,  /**< ��������·��·�澯 */
        AlARM_TYPE_SHORT_CIRCUIT_RESUME = 211  ,  /**< ��������·��·�澯�ָ� */
        AlARM_TYPE_STOR_FULL_PRE = 212         ,  /**< �洢�������澯 */
        AlARM_TYPE_STOR_FULL = 213             ,  /**< ���洢�澯 */
        AlARM_TYPE_STOR_FAILED = 214           ,  /**< �洢��дʧ�ܸ澯 */
        AlARM_TYPE_STOR_FAILED_RESUME = 215    ,  /**< �洢��дʧ�ܸ澯�ָ� */
        AlARM_TYPE_DEVICE_ONLINE = 216         ,  /**< �豸���߸澯 */
        AlARM_TYPE_DEVICE_OFFLINE = 217        ,  /**< �豸���߸澯 */
        AlARM_TYPE_BREAKER_CIRCUIT_RESUME = 219,  /**< ��������·��·�澯�ָ� */

        AlARM_TYPE_STREAM_STOR_CAM_WARN = 222,  /**< ������洢ֹͣ�澯 */

        AlARM_TYPE_EXT_STOR_FULL_PRE = 223,  /**< Զ�˴洢�������澯 */
        AlARM_TYPE_EXT_STOR_FULL = 224,  /**< Զ�˴洢���澯 */
        AlARM_TYPE_EXT_STOR_FAILED = 225,  /**< Զ�˴洢��дʧ�ܸ澯 */
        AlARM_TYPE_EXT_STOR_FAILED_RESUME = 226,  /**< Զ�˴洢��дʧ�ܸ澯�ָ� */

        AlARM_TYPE_STOR_NO_ENOUGH_SPACE = 229,  /**< ��������ڴ��̴洢�ռ䲻��澯 */
        AlARM_TYPE_STOR_DEL_FILE_FAILED = 230,  /**< ɾ�����ļ�ʧ�ܸ澯 */

        AlARM_TYPE_BEHAVIOR = 231,  /**< ��Ϊ�澯 */
        AlARM_TYPE_BEHAVIOR_RESUME = 232,  /**< ��Ϊ�澯�ָ� */

        ALARM_TYPE_STREAM_BREAK = 233,  /**< ����������澯 */

        AlARM_TYPE_SOUND_ABNORMAL = 241,  /**< �쳣�����澯 */
        AlARM_TYPE_SOUND_ABNORMAL_RESUME = 242,  /**< �쳣�����澯�ָ� */

        ALARM_TYPE_CROSS_LINE = 301,  /**< �������ܰ��� */
        ALARM_TYPE_INTROSION_ZONE = 302,  /**< �������� */
        ALARM_TYPE_ACCESS_ZONE = 303,  /**< �������� */
        ALARM_TYPE_LEAVE_ZONE = 304,  /**< �뿪���� */
        ALARM_TYPE_HOVER_ZONE = 305,  /**< �����ǻ� */
        ALARM_TYPE_OVER_FENCE = 306,  /**< ��ԽΧ�� */
        ALARM_TYPE_CARE_ARTICLE = 307,  /**< ��Ʒ���� */
        ALARM_TYPE_REMAIN_ARTICLE = 308,  /**< ��Ʒ���� */



        /* �ֹ��澯 401~ */
        AlARM_TYPE_IMPERATIVE_EVENT = 401,  /**< �����澯 */

        AlARM_TYPE_NM_PROTECT_EVENT = 800,  /**< ����N+M������澯, ��Ҫ���ڿ���N+M�����澯����,
                                                     ���ĺ��൱�ڶ����˴洢ʧ�ܡ��洢ֹͣ�����澯 */
        /* Begin added by baoyihui02795, 2011-04-28 of ������Ŀ */
        /** ����ҵ��澯 2001~ */
        ALARM_TYPE_DISPOSITION_STOLEN_VEHICLE = 2001,   /**< ������ */
        ALARM_TYPE_DISPOSITION_ROBBED_VEHICLE = 2002,   /**< ������ */
        ALARM_TYPE_DISPOSITION_SUSPICION_VEHICLE = 2003,   /**< ���ɳ� */
        ALARM_TYPE_DISPOSITION_TRAFFIC_VIOLATION = 2004,   /**< ��ͨΥ���� */
        ALARM_TYPE_DISPOSITION_EMERGENCY_SURVEILLANCE = 2005,   /**< ������س� */
        ALARM_TYPE_VEHICLE_BLACKLIST = 2006,   /**< ������ */
        ALARM_TYPE_OTHER_VEHICLE_ALARM = 2007,   /**< ��������Υ����Ϊ */
        ALARM_TYPE_P2P_SPEED_DETECTION = 2008,   /**< �������Υ�� */
        ALARM_TYPE_NOT_WHITELIST = 2009,   /**< �ǰ��������� */
        /* End added by baoyihui02795, 2011-04-28 of ������Ŀ */
        AlARM_TYPE_MAX,                         /**< ���ֵ */

        AlARM_TYPE_ALL = 0xFFFE,           /**< ���и澯�������� */
        AlARM_TYPE_INVALID = 0xFFFF            /**< ��Чֵ */
    };


    public enum IMOS_PICTURE_FORMAT_E
    {
        IMOS_PF_PAL = 0,                            /* PAL ��ʽ */
        IMOS_PF_NTSC,                               /* NTSC ��ʽ */
        IMOS_PF_720P24HZ,
        IMOS_PF_720P25HZ,
        IMOS_PF_720P30HZ,
        IMOS_PF_720P50HZ,
        IMOS_PF_720P60HZ,
        IMOS_PF_1080I48HZ,
        IMOS_PF_1080I50HZ,
        IMOS_PF_1080I60HZ,
        IMOS_PF_1080P24HZ,
        IMOS_PF_1080P25HZ,
        IMOS_PF_1080P30HZ,
        IMOS_PF_1080P50HZ,
        IMOS_PF_1080P60HZ,
        IMOS_PF_AUTO,
        IMOS_PF_INVALID
    };

    /* ǰ���豸���� */
    public enum IMOS_DEVICE_TYPE_E
    {
        IMOS_DT_EC1001_HF = 0,
        IMOS_DT_EC1002_HD,                          /* ���ͺ� �� EC1004_HC ���ö��ɣ���Ӧ EC1004-HC[2CH] */
        IMOS_DT_EC1004_HC,
        IMOS_DT_EC2004_HF,
        IMOS_DT_ER3304_HF,
        IMOS_DT_ER3308_HD = 5,
        IMOS_DT_ER3316_HC,
        IMOS_DT_DC1001_FF = 7,
        IMOS_DT_EC3016_HF,
        IMOS_DT_ISC3100_ER,
        IMOS_DT_EC1001_EF = 10,
        IMOS_DT_EC3004_HF_ER,
        IMOS_DT_EC3008_HD_ER,

        /* ö��ֵ 200 -- 500 */
        IMOS_DT_EC2016_HC = 200,
        IMOS_DT_EC2016_HC_8CH,
        IMOS_DT_EC2016_HC_4CH,
        IMOS_DT_EC1501_HF,                          /* 6437��˫�� */
        IMOS_DT_EC1004_MF,                          /* ��·EC */
        IMOS_DT_ECR2216_HC,                         /* ECRԤ�У�1U */
        IMOS_DT_EC1304_HC,                          /* ��PCMCIA */

        /* ö��ֵ 500 -- 800 */
        IMOS_DT_DC3016_FC = 500,                    /* ĿǰV1R5��2U����ISC3000-M������� */
        IMOS_DT_DC2016_FC,                          /* DC3016-FC���ɱ� */
        IMOS_DT_DC1016_MF,                          /* ��·DC */
        IMOS_DT_DC2004_FF,                          /* �����־���Ķ�·DC */

        IMOS_DT_EC1001 = 1000,                      /* R1 ����壬Ϊ�˷�����չ����1000��ʼ���� */
        IMOS_DT_DC1001 = 1001,                      /* R1 ����� */
        IMOS_DT_EC1101_HF = 1002,
        IMOS_DT_EC1102_HF = 1003,
        IMOS_DT_EC1801_HH = 1004,
        IMOS_DT_DC1801_FH = 1005,

        /* OEM��Ʒ�ͺ� */
        IMOS_DT_VR2004 = 10003,
        IMOS_DT_R1000 = 10203,
        IMOS_DT_VL2004 = 10503,
        IMOS_DT_VR1102 = 11003,
        /* IPC��Ʒ�ͺ� */
        IMOS_DT_HIC5201 = 12001,
        IMOS_DT_HIC5221 = 12002,
        IMOS_DT_BUTT
    };

    /**
* @enum tagMediaFileFormat
* @brief ý���ļ���ʽö�ٶ���
* @attention ��
*/
    public enum XP_MEDIA_FILE_FORMAT_E
    {
        XP_MEDIA_FILE_TS = 0,              /**< TS��ʽ��ý���ļ� */
        XP_MEDIA_FILE_FLV = 1               /**< FLV��ʽ��ý���ļ� */
    };

    public enum IMOS_STREAM_RELATION_SET_E
    {
        IMOS_SR_MPEG4_MPEG4 = 0,                    /* MPEG4[������] + MPEG4[������] */
        IMOS_SR_H264_SHARE = 1,                     /* H.264[������] */
        IMOS_SR_MPEG2_MPEG4 = 2,                    /* MPEG2[������] + MPEG4[������] */
        IMOS_SR_H264_MJPEG = 3,                     /* H.264[������] + MJPEG[������] */
        IMOS_SR_MPEG4_SHARE = 4,                    /* MPEG4[������] */
        IMOS_SR_MPEG2_SHARE = 5,                    /* MPEG2[������] */
        IMOS_SR_STREAM_MPEG4_8D1 = 8,          /* MPEG4[������_D1] 8D1 �ײ�*/
        IMOS_SR_MPEG2_MPEG2 = 9,                    /* MPEG2[������] + MPEG2[������] */
        IMOS_SR_H264_H264 = 11,                     /* H.264[������] + H.264[������] */

        IMOS_SR_BUTT
    };


    /**
 * @enum tagIMOSFavoriteStream
 * @brief  �����ò�������
 * @attention ��
 */
    public enum IMOS_FAVORITE_STREAM_E
    {
        IMOS_FAVORITE_STREAM_ANY = 0,        /**< ��ָ�� */
        IMOS_FAVORITE_STREAM_PRIMARY = 1,        /**< ָ������ */
        IMOS_FAVORITE_STREAM_SECONDERY = 2,        /**< ָ������ */
        IMOS_FAVORITE_STREAM_THIRD = 3,        /**< ָ������ */
        IMOS_FAVORITE_STREAM_FOURTH = 4,        /**< ָ������ */
        IMOS_FAVORITE_STREAM_FIFTH = 5,        /**< ָ������ */
        IMOS_FAVORITE_STREAM_BI_AUDIO = 6,        /**< ָ�������Խ� */
        IMOS_FAVORITE_STREAM_VOICE_BR = 7,        /**< ָ�������㲥 */
        IMOS_FAVORITE_STREAM_BUTT,
        IMOS_FAVORITE_STREAM_INVALID = 0xFFFF    /**< ��Чֵ */
    }

    public enum XP_PROTOCOL_E
    {
        XP_PROTOCOL_UDP = 0,                   /**< UDPЭ�� */
        XP_PROTOCOL_TCP = 1,                   /**< TCPЭ��Client��*/
        XP_PROTOCOL_TCP_SERVER = 2             /**< TCPЭ��Server��*/
    }

    /**
* @enum tagDownMediaSpeed
* @brief ý�����������ٶȵ�ö�ٶ���
* @attention ��
*/
    public enum XP_DOWN_MEDIA_SPEED_E
    {
        XP_DOWN_MEDIA_SPEED_ONE = 0,            /**< һ��������ý���ļ� */
        XP_DOWN_MEDIA_SPEED_TWO = 1,            /**< ����������ý���ļ� */
        XP_DOWN_MEDIA_SPEED_FOUR = 2,           /**< �ı�������ý���ļ� */
        XP_DOWN_MEDIA_SPEED_EIGHT = 3           /**< �˱�������ý���ļ� */
       
    }


    /// <summary>
    /// ͨ�õ���̨����ö��ֵ
    /// </summary>
    public enum PTZ_CMD_E
    {
        PTZ_UP,
        PTZ_UP_STOP,
        PTZ_DOWN,
        PTZ_DOWN_STOP,
        PTZ_LEFT,
        PTZ_LEFT_STOP,
        PTZ_RIGHT,
        PTZ_RIGHT_STOP,
        PTZ_LEFT_UP,
        PTZ_LEFT_UP_STOP,
        PTZ_LEFT_DOWN,
        PTZ_LEFT_DOWN_STOP,
        PTZ_RIGHT_UP,
        PTZ_RIGHT_UP_STOP,
        PTZ_RIGHT_DOWN,
        PTZ_RIGHT_DOWN_STOP,

        PTZ_ALL_STOP,           /**< ȫͣ */

        PTZ_IRIS_OPEN,          /**< ��Ȧ�� */
        PTZ_IRIS_OPEN_STOP,
        PTZ_IRIS_CLOSE,         /**< ��Ȧ�� */
        PTZ_IRIS_CLOSE_STOP,

        PTZ_FOCUS_FAR,          /**< �۽�Զ */
        PTZ_FOCUS_FAR_STOP,
        PTZ_FOCUS_NEAR,         /**< �۽��� */
        PTZ_FOCUS_NEAR_STOP,

        PTZ_ZOOM_TELE,          /**< �Ŵ� */
        PTZ_ZOOM_TELE_STOP,
        PTZ_ZOOM_WIDE,          /**< ��С */
        PTZ_ZOOM_WIDE_STOP,

        PTZ_PRE_SAVE,           /**< ����Ԥ��λ */
        PTZ_PRE_CALL,           /**< ����Ԥ��λ */
        PTZ_PRE_DELETE,         /**< ɾ��Ԥ��λ */

        PTZ_BRUSH_ON,           /**< ��ˢ�� */
        PTZ_BRUSH_OFF,
        PTZ_LIGHT_ON,           /**< �ƹ⿪ */
        PTZ_LIGHT_OFF,
        PTZ_HEAT_ON,            /**< ���ȿ� */
        PTZ_HEAT_OFF,

        PTZ_CRUISE_START,       /**< ����Ѳ�� */
        PTZ_CRUISE_STOP,        /**< ֹͣѲ�� */
    }

    /// <summary>
    /// �澯��������
    /// </summary>
    public enum SUBSCRIBE_PUSH_TYPE_E
    {
        SUBSCRIBE_PUSH_TYPE_ALL,         //���ܸ澯���ͺ��豸״̬����  
        SUBSCRIBE_PUSH_TYPE_ALARM,       //ֻ���ո澯����  
        SUBSCRIBE_PUSH_TYPE_ALARM_STATUS,//ֻ�����豸״̬����  
        SUBSCRIBE_PUSH_TYPE_MAX,         //   
        SUBSCRIBE_PUSH_TYPE_INVALID
    }

    /** �ص�����������Ϣ���� */
    public enum CALL_BACK_PROC_TYPE_E : uint
    {
        PROC_TYPE_DEV_STATUS = 0,            /**< �豸״̬����Ӧ�ṹ : AS_STAPUSH_UI_S */
        PROC_TYPE_ALARM = 1,            /**< �澯����Ӧ�ṹ : AS_ALARMPUSH_UI_S */
        PROC_TYPE_DEV_CAM_SHEAR = 2,            /**< �������������Ӧ�ṹ : AS_DEVPUSH_UI_S */
        PROC_TYPE_MONITOR_BE_REAVED = 3,            /**< ʵ������ռ����Ӧ�ṹ : CS_MONITOR_REAVE_NOTIFY_S */
        PROC_TYPE_SWITCH_BE_REAVED = 4,            /**< ���б���ռ����Ӧ�ṹ : CS_SWITCH_REAVE_NOTIFY_S */
        PROC_TYPE_MONITOR_BE_STOPPED = 5,            /**< ʵ����ֹͣ����Ӧ�ṹ : CS_MONITOR_REAVE_NOTIFY_S */
        PROC_TYPE_SWITCH_BE_STOPPED = 6,            /**< ���б�ֹͣ����Ӧ�ṹ : CS_SWITCH_REAVE_NOTIFY_S */
        PROC_TYPE_VOCSRV_BE_REAVED = 7,            /**< ��������ռ����Ӧ�ṹ : CS_VOCSRV_REAVE_NOTIFY_S */
        PROC_TYPE_PTZ_EVENT = 8,            /**< ��̨�¼�֪ͨ����Ӧ�ṹ : CS_PTZ_REAVE_NOTIFY_S */
        PROC_TYPE_TRB_PROC = 9,            /**< ���ϴ���֪ͨ����Ӧ�ṹ : CS_NOTIFY_UI_TRB_EVENT_PROC_S */
        PROC_TYPE_SRV_SETUP = 10,           /**< ���ϻָ�ҵ����֪ͨ����Ӧ�ṹ : CS_NOTIFY_UI_SRV_SETUP_S */
        PROC_TYPE_XP_ALARM_SETUP = 11,           /**< �澯������XP����֪ͨ����Ӧ�ṹ : CS_NOTIFY_UI_SRV_SETUP_S */

        PROC_TYPE_LOGOUT = 12,           /**< �˳���½����Ӧ�ṹ :�� */

        PROC_TYPE_MEDIA_PARAM_CHANGE = 13,           /**< ý������ı䣬��Ӧ�ṹ : CS_NOTIFY_UI_MEDIA_PARAM_CHANGE_PROC_S */

        PROC_TYPE_EXDOMAIN_STATUS = 14,           /**< ����״̬����Ӧ�ṹ : AS_EXDOMAIN_STAPUSH_UI_S */

        PROC_TYPE_BACKUP_DATA_FINISH = 15,           /**< ��Ϣ�������֪ͨ, ��Ӧ�ṹ : CS_BACKUP_FINISH_INFO_S */

        PROC_TYPE_TL_EVENT = 16,           /**< ͸��ͨ���¼�֪ͨ����Ӧ�ṹ : CS_TL_REAVE_NOTIFY_S */

        PROC_TYPE_SALVO_UNIT_EVENT = 17,           /**< ����ʾ��Ԫ�¼�֪ͨ, ��Ӧ�ṹ : CS_NOTIFY_SALVO_UNIT_EVENT_S */
        PROC_TYPE_SALVO_EVENT = 18,           /**< ����ҵ���¼�֪ͨ, ��Ӧ�ṹ : CS_NOTIFY_UI_SALVO_EVENT_S */
        PROC_TYPE_START_XP_SALVO = 19,           /**< ֪ͨUI��������Ѳ������ʾ, ��Ӧ�ṹ: CS_NOTIFY_START_XP_SALVO_S */

        PROC_TYPE_VODWALL_BE_REAVED = 20,           /**< ֪ͨ�ط���ǽ����ռ����Ӧ�ṹ��CS_VODWALL_REAVE_NOTIFY_S */
        PROC_TYPE_VODWALL_BE_STOPPED = 21,           /**< ֪ͨ�ط���ǽ��ֹͣ����Ӧ�ṹ��CS_VODWALL_REAVE_NOTIFY_S */

        PROC_TYPE_VOD_BE_REAVED = 22,           /**< �طű���ռ����Ӧ�ṹ : CS_VOD_REAVE_NOTIFY_S */

        PROC_TYPE_DOMAIN_SYN_RESULT = 23,           /**< ���ͬ���Ľ������Ӧ�ṹ : AS_DOMAIN_SYN_PUSHUI_S */

        PROC_TYPE_VOC_REQ = 24,           /**< �ͻ����������󣬶�Ӧ�ṹ : CS_VOC_REQ_NOTIFY_S */
        PROC_TYPE_VOC_STATE_NOTIFY = 25,           /**< ����ҵ��״̬֪ͨ����Ӧ�ṹ : CS_VOC_STATE_NOTIFY_S */

        /*******************************************************************************
        SS�������� �����ص�����
        *******************************************************************************/
        PROC_TYPE_ACCEPT_SPEAK_YESORNO = 100,           /**< �������룬 ��Ӧ�ṹ ��CONF_SITE_INFO_EX_S */
        PROC_TYPE_CONF_STATUS_CHANGE = 101,           /**< ����״̬�ı䣬 ��Ӧ�ṹ ��CONF_STATUS_INFO_EX_S ��������ڻ����ҷ����һ�����ڣ��ϱ�����δ��ʼ/�����ϱ������Ѿ����� */
        PROC_TYPE_DEVICE_CODE_CHANGE = 102,           /**< �豸����ı䣬 ��Ӧ�ṹ ��DEVICE_CODE_CHANGE_INFO_EX_S */
        PROC_TYPE_DEVICE_CHANGE = 103,           /**< �ն��豸������Ϣ�� �������豸������ɾ��ʱ�� �ϱ�������Ϣ�� ��Ӧ�Ľṹ ��DEVICE_CHANGE_INFO_EX_S */
        PROC_TYPE_MODIFY_TERM = 104,           /**< �޸��ն���Ϣ�� ��Ӧ�Ľṹ ��MODIFY_TERM_REP_EX_S */
        PROC_TYPE_CHAIR_CHANGE = 105,           /**< ��ǰ��ϯ�����ı䣬 ��ϯ�᳡�ͷ���᳡����Ϊ�ա���Ӧ�Ľṹ ��CONF_SITE_INFO_EX_S */
        PROC_TYPE_SPEAKER_CHANGE = 106,           /**< ��ǰ�����˷����ı䣬 ��Ӧ�Ľṹ ��CONF_SITE_INFO_EX_S */
        PROC_TYPE_TERM_STATUS_CHANGE = 107,           /**< �᳡�ն�״̬�ı䣬 ��Ӧ�Ľṹ ��TERM_STATUS_CHANGE_EX_S */
        PROC_TYPE_DELAY_CONF = 108,           /**< �ӳٻ��飬 ��Ӧ�ṹ ��DELAY_CONF_INFO_EX_S */
        PROC_TYPE_SYNCHRONIZE_WITH_WEB = 109,           /**< �ϱ��㲥�᳡�� ��ϯ�������ۿ��᳡ ��Ӧ�Ľṹ �� MC_SYNCHRONIZE_WITH_WEB_EX_S  */
        PROC_TYPE_MCU_BACKUP_CHANGE_TO_MASTER = 110,    /**< MCU����֪ͨ�� ��Ӧ�ṹ ��BACKUP_MCU_REPORT_S  */
        PROC_TYPE_SEND_ROLE_SITE_CHANGE = 111,           /**< ��ǰ���������߱仯֪ͨ�� ��Ӧ�ṹ ��CONF_SEND_ROLE_SITE_CHANGE_S  */
        PROC_TYPE_AUTOMULTIPIC_CHANGE = 112,           /**< �໭���Զ��л�ֵ�ı�֪ͨ�� ��Ӧ�ṹ ��CONF_AUTOMULTIPIC_CHANGE_S  */
        PROC_TYPE_SET_TURN_BROADCAST_CHANGE = 113,       /**< ���û�����ѯģʽ�ı�֪ͨ�� ��Ӧ�ṹ ��CONF_SET_TURN_BROADCAST_CHANGE_S */
        PROC_TYPE_SET_PIC_MODE_CHANGE = 114,           /**< ���û���ģʽ�ı�֪ͨ�� ��Ӧ�ṹ ��CONF_SET_PIC_MODE_CHANGE_S */
        PROC_TYPE_MCU_SYNC_STATUS_CHANGE = 115,          /**< MCUͬ��״̬�ı�֪ͨ�� ��Ӧ�ṹ ��MCU_SYNC_STATUS_CHANGE_S */
        PROC_TYPE_CUR_BROADCAST_CHANGE = 116,          /**< ��ǰʵ�ʹ㲥�᳡�ı�֪ͨ����Ӧ�ṹ��CUR_BROADCAST_INFO_EX_S */
        PROC_TYPE_CUR_CHAIR_BROWSE_CHANGE = 117,     /**< ��ǰ��ϯ������ʵ�ʹۿ��Ļ᳡�ı�֪ͨ����Ӧ�ṹ��CUR_CHAIR_BROWSE_INFO_EX_S */
        PROC_TYPE_CONF_FECC_CHANGE = 118,          /**< ��ǰFECC�����߻򱻿��߱仯֪ͨ����Ӧ�ṹ��CONF_FECC_CHANGE_S */
        PROC_TYPE_CONF_MCU_BACKUP_CHANGE = 119,     /**< ��ǰ������MCU���ݱ仯֪ͨ����Ӧ�ṹ��CONF_MCU_BACKUP_CHANGE_S */
        PROC_TYPE_CALL_SITE_RESULT = 120,          /**< ���л᳡���֪ͨ����Ӧ�ṹ��CALL_SITE_INFO_EX_S */
        PROC_TYPE_GK_REG_STATE = 121,          /**< GKע����֪ͨ����Ӧ�ṹ��GK_REG_STATE_INFO_EX_S */
        PROC_TYPE_MG_SESSION_STATUS_CHANGE = 122,     /**< �ն˻Ự״̬����Ӧ�ṹ��MG_SESSION_STATUS_EX_S */
        PROC_TYPE_MAX,                                   /**< �ص�����������Ϣ�������ֵ */
        PROC_TYPE_INVALID = 0xFFFFFFFE    /**< ��Чֵ */
    }




    /**
* @enum tagRunInfoType
* @brief �ϱ���Ϣ���͵�ö�ٶ���
* @attention ��
*/
    public enum XP_RUN_INFO_TYPE_E
    {
        XP_RUN_INFO_SERIES_SNATCH = 0,        /**< ����ץ�Ĺ������ϱ�������Ϣ */
        XP_RUN_INFO_RECORD_VIDEO = 1,         /**< ����¼��������ϱ�������Ϣ */
        XP_RUN_INFO_MEDIA_PROCESS = 2,        /**< ��Ƶý�崦������е��ϱ�������Ϣ */
        XP_RUN_INFO_DOWN_MEDIA_PROCESS = 3,   /**< ý�������ع������ϱ�������Ϣ */
        XP_RUN_INFO_VOICE_MEDIA_PROCESS = 4,  /**< ����ý�崦������е��ϱ�������Ϣ */
        XP_RUN_INFO_RTSP_PROTOCOL = 5,        /**< RTSPЭ��������еĴ�����Ϣ */
        XP_RUN_INFO_DOWN_RTSP_PROTOCOL = 6,   /**< ����¼�������RTSPЭ��Ĵ�����Ϣ */
        XP_RUN_INFO_SIP_LIVE_TIMEOUT = 7,     /**< SIPע�ᱣ�ʱ */
        XP_RUN_INFO_PASSIVE_MONITOR = 8,      /**< ����ʵ��ֹͣ������Ϣ */
        XP_RUN_INFO_PASSIVE_START_MONITOR = 9,/**< ����ʵ������������Ϣ */
        XP_RUN_INFO_MEDIA_NOT_IDENTIFY = 10,  /**< �����޷�ʶ�� */
        XP_RUN_INFO_RECV_PACKET_NUM = 11,     /**< �����ڽ��յ��İ��� */
        XP_RUN_INFO_RECV_BYTE_NUM = 12,       /**< �����ڽ��յ����ֽ��� */
        XP_RUN_INFO_VIDEO_FRAME_NUM = 13,     /**< �����ڽ�������Ƶ֡�� */
        XP_RUN_INFO_AUDIO_FRAME_NUM = 14,     /**< �����ڽ�������Ƶ֡�� */
        XP_RUN_INFO_LOST_PACKET_RATIO = 15,   /**< �����ڶ�����ͳ����Ϣ����λΪ0.01%�� */
        XP_RUN_INFO_MEDIA_PLAY_PROGRESS = 16, /**< ý����Я���Ľ�����Ϣ */
        XP_RUN_INFO_MEDIA_PLAY_END = 17,      /**< ý����Я���Ĳ��Ž��� */
        XP_RUN_INFO_MEDIA_ABNORMAL = 18       /**< ý�崦���쳣 */
    }

    public enum MW_PTZ_CMD_E
    {
        MW_PTZ_IRISCLOSESTOP = 0x0101, /**< ��Ȧ��ֹͣ */
        MW_PTZ_IRISCLOSE = 0x0102,         /**< ��Ȧ�� */
        MW_PTZ_IRISOPENSTOP = 0x0103,   /**< ��Ȧ��ֹͣ */
        MW_PTZ_IRISOPEN = 0x0104,   /**< ��Ȧ�� */

        MW_PTZ_FOCUSNEARSTOP = 0x0201, /**< ���ۼ�ֹͣ */
        MW_PTZ_FOCUSNEAR = 0x0202,    /**< ���ۼ� */
        MW_PTZ_FOCUSFARSTOP = 0x0203,/**< Զ�ۼ� ֹͣ*/
        MW_PTZ_FOCUSFAR = 0x0204,        /**< Զ�ۼ� */

        MW_PTZ_ZOOMTELESTOP = 0x0301,/**< �Ŵ�ֹͣ */
        MW_PTZ_ZOOMTELE = 0x0302,/**< �Ŵ� */
        MW_PTZ_ZOOMWIDESTOP = 0x0303,/**< ��Сֹͣ */
        MW_PTZ_ZOOMWIDE = 0x0304,/**< ��С */

        MW_PTZ_TILTUPSTOP = 0x0401,/**< ����ֹͣ */
        MW_PTZ_TILTUP = 0x0402,/**< ���� */
        MW_PTZ_TILTDOWNSTOP = 0x0403,/**< ����ֹͣ */
        MW_PTZ_TILTDOWN = 0x0404,/**< ���� */

        MW_PTZ_PANRIGHTSTOP = 0x0501,/**< ����ֹͣ */
        MW_PTZ_PANRIGHT = 0x0502,/**< ���� */
        MW_PTZ_PANLEFTSTOP = 0x0503,/**< ����ֹͣ */
        MW_PTZ_PANLEFT = 0x0504,/**< ���� */

        MW_PTZ_PRESAVE = 0x0601,/**< Ԥ��λ���� */
        MW_PTZ_PRECALL = 0x0602,/**< Ԥ��λ���� */
        MW_PTZ_PREDEL = 0x0603,/**< Ԥ��λɾ�� */

        MW_PTZ_LEFTUPSTOP = 0x0701,/**< ����ֹͣ */
        MW_PTZ_LEFTUP = 0x0702,/**< ���� */
        MW_PTZ_LEFTDOWNSTOP = 0x0703,/**< ����ֹͣ */
        MW_PTZ_LEFTDOWN = 0x0704,/**< ���� */

        MW_PTZ_RIGHTUPSTOP = 0x0801,/**< ����ֹͣ */
        MW_PTZ_RIGHTUP = 0x0802,/**< ���� */
        MW_PTZ_RIGHTDOWNSTOP = 0x0803,/**< ����ֹͣ */
        MW_PTZ_RIGHTDOWN = 0x0804,/**< ���� */

        MW_PTZ_ALLSTOP = 0x0901,/**< ȫͣ������ */

        MW_PTZ_BRUSHON = 0x0A01,/**< ��ˢ�� */
        MW_PTZ_BRUSHOFF = 0x0A02,/**< ��ˢ�� */

        MW_PTZ_LIGHTON = 0x0B01,/**< �ƿ� */
        MW_PTZ_LIGHTOFF = 0x0B02,/**< �ƹ� */

        MW_PTZ_HEATON = 0x0C01,/**< ���ȿ� */
        MW_PTZ_HEATOFF = 0x0C02,/**< ���ȹ� */

        MW_PTZ_INFRAREDON = 0x0D01,/**< ���⿪ */
        MW_PTZ_INFRAREDOFF = 0x0D02,/**< ����� */

        MW_PTZ_SCANCRUISE = 0x0E01,/**< ��̨����ɨè */
        MW_PTZ_SCANCRUISESTOP = 0x0E02,/**< ��̨����ɨè */

        MW_PTZ_TRACKCRUISE = 0x0F01,/**<  ��̨�켣Ѳ�� */
        MW_PTZ_TRACKCRUISESTOP = 0x0F02,/**<  ��̨�켣Ѳ�� */

        MW_PTZ_PRESETCRUISE = 0x1001,/**<  ��̨��Ԥ��λѲ�� ���������ֲ�����̨ģ������ */
        MW_PTZ_PRESETCRUISESTOP = 0x1002,/**<  ��̨��Ԥ��λѲ�� ֹͣ���������ֲ�����̨ģ������ */

        PTZ_RELEASE,            /**< �ͷ���̨ */
        PTZ_LOCK,               /**< ������̨ */
        PTZ_UNLOCK,             /**< ������̨ */
        MW_PTZ_CMD_BUTT

    }


    public enum IMOS_TYPE_E
    {
        IMOS_TYPE_ORG = 1,                     /**< ��֯�� */
        IMOS_TYPE_OUTER_DOMAIN = 2,            /**< ���� */
        IMOS_TYPE_LOCAL_DOMAIN = 3,            /**< ���� */

        IMOS_TYPE_DM = 11,                     /**< DM */
        IMOS_TYPE_MS = 12,                     /**< MS */
        IMOS_TYPE_VX500 = 13,                  /**< VX500 */
        IMOS_TYPE_MONITOR = 14,                /**< ������ */

        IMOS_TYPE_EC = 15,                     /**< EC */
        IMOS_TYPE_DC = 16,                     /**< DC */

        IMOS_TYPE_GENERAL = 17,                /**< ͨ���豸 */

        IMOS_TYPE_MCU = 201,                   /**< MCU */
        IMOS_TYPE_MG = 202,                    /**< MG */

        IMOS_TYPE_CAMERA = 1001,               /**< ����� */
        IMOS_TYPE_ALARM_SOURCE = 1003,         /**< �澯Դ */

        IMOS_TYPE_STORAGE_DEV = 1004,          /**< �洢�豸 */
        IMOS_TYPE_TRANS_CHANNEL = 1005,        /**< ͸��ͨ�� */

        IMOS_TYPE_ALARM_OUTPUT = 1200,         /**< �澯��� */

        IMOS_TYPE_GUARD_TOUR_RESOURCE = 2001,  /**< ������Դ */
        IMOS_TYPE_GUARD_TOUR_PLAN = 2002,      /**< ���мƻ� */
        IMOS_TYPE_MAP = 2003,                  /**< ��ͼ */

        IMOS_TYPE_XP = 2005,                   /**< XP */
        IMOS_TYPE_XP_WIN = 2006,               /**< XP���� */
        IMOS_TYPE_GUARD_PLAN = 2007,           /**< �����ƻ� */

        IMOS_TYPE_DEV_ALL = 2008,              /**< ���е��豸����(EC/DC/MS/DM/VX500/����ͷ/������) */
        IMOS_TYPE_TV_WALL = 3001,              /**< ����ǽ */

        IMOS_TYPE_CONFERENCE = 4001,           /**< ������Դ */

        IMOS_TYPE_MAX
    }
    /**
* @enum tagScreenSplitType
* @brief ��������
* @attention
*/
    public enum SCREEN_SPLIT_TYPE_E
    {
        SCREEN_SPLIT_TYPE_ONE = 1,                 /** ������ */
        SCREEN_SPLIT_TYPE_FOUR = 4,                 /** 4���� */
        SCREEN_SPLIT_TYPE_SIX = 6,                 /** 6���� */
        SCREEN_SPLIT_TYPE_EIGHT = 8,                 /** 8���� */
        SCREEN_SPLIT_TYPE_NINE = 9,                 /** 9���� */
        SCREEN_SPLIT_TYPE_TEN = 10,                /** 10���� */
        SCREEN_SPLIT_TYPE_THIRTEEN = 13,                /** 13���� */
        SCREEN_SPLIT_TYPE_SIXTEEN = 16,                /** 16���� */
        SCREEN_SPLIT_TYPE_MAX,                                              /**< ���ֵ */
        SCREEN_SPLIT_TYPE_INVALID = 0x0FFFFFFF       /**< ��Чֵ */
    }

    /**
* @enum tagSplitScrMode
* @brief ����������ģʽ
* @attention
*/
    public enum SPLIT_SCR_MODE_E
    {
        SPLIT_SCR_MODE_0 = 0,    /**< �����ַ� */
        SPLIT_SCR_MODE_1 = 1,    /**< ȫ�� */
        SPLIT_SCR_MODE_2 = 2,    /**< 2 ����*/
        SPLIT_SCR_MODE_3 = 3,    /**< 3 ����*/
        SPLIT_SCR_MODE_4 = 4,    /**< 4���� */
        SPLIT_SCR_MODE_5 = 5,    /**< 5 ����*/
        SPLIT_SCR_MODE_6 = 6,    /**< 6���� */
        SPLIT_SCR_MODE_7 = 7,    /**< 7 ����*/
        SPLIT_SCR_MODE_8 = 8,    /**< 8 ���� */
        SPLIT_SCR_MODE_9 = 9,    /**< 9 ���� */
        SPLIT_SCR_MODE_10 = 10,   /**< 10 ���� */
        SPLIT_SCR_MODE_11 = 11,    /**< 11 ����*/
        SPLIT_SCR_MODE_12 = 12,    /**< 12 ����*/
        SPLIT_SCR_MODE_13 = 13,   /**< 13 ���� */
        SPLIT_SCR_MODE_14 = 14,    /**< 14 ����*/
        SPLIT_SCR_MODE_15 = 15,    /**< 15 ����*/
        SPLIT_SCR_MODE_16 = 16,   /**< 16 ���� */
        SPLIT_SRC_MODE_25 = 25,   /**< 25����*/
        SPLIT_SRC_MODE_36 = 36,   /**< 36����*/

        SPLIT_SCR_MODE_MAX,                     /**< ���ֵ */

        /* Begin modified by l01420 for MPPD08374, 2014-4-8 */
        /**<SPLIT_SCR_MODE_MAXö��ֵ���϶������·�������ģʽ��
        �����������ֵ��ʾ����ķ���ģʽ(�������ظ���ģʽ��ͬ)*/
        SPLIT_SCR_MODE_80 = 80,    /**< 8 ���� ����ģʽ */

        SPLIT_SCR_MODE_LIMIT_VALUE,/**<����ģʽ���ֵ*/
        /* End modified by l01420 for MPPD08374, 2014-4-8 */

        SPLIT_SCR_MODE_INVALID = 0x0FFFFFFF    /**< ��Чֵ */
    }

    /* added by z06806 for tollgate struct */
    /**
     * @struct tagReservedInfo
     * @brief Ԥ���ֶνṹ
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct RESERVED_INFO_S
    {
        /** Ԥ���ֶ�1:��ʹ�ã����ڱ�ʾ����ԭ�� */
        public UInt32 ulReserved1;

        /** Ԥ���ֶ�2 */
        public UInt32 ulReserved2;

        /** Ԥ���ֶ�3 */
        public UInt32 ulReserved3;

        /** Ԥ���ֶ�4 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RESERVED_LEN)]
        public byte[] szReserved4;

        /** Ԥ���ֶ�5 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RESERVED_LEN)]
        public byte[] szReserved5;

        /** Ԥ���ֶ�6 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RESERVED_LEN)]
        public byte[] szReserved6;
    }


    /**
     * @struct tagA8Info
     * @brief A8��Ϣ
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct A8_INFO_S
    {
        /** A8���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szA8Code;

        /** A8 EC���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szA8ECCode;

        /** A8 DC���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szA8DCCode;

        /** A8���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szA8Name;

        /** A8���ͣ�ȡֵΪ#IMOS_DEVICE_TYPE_E */
        public UInt32 ulA8Type;

        /** �豸��ַ���ͣ�1-IPv4 2-IPv6 */
        public UInt32 ulDevaddrtype;

        /** �豸��ַ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szDevAddr;

        /** ������֯���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** ������֯���ƣ���ѯ���أ���������¿��Բ��� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szOrgName;

        /** �豸�Ƿ�����ȡֵΪ#IMOS_DEV_STATUS_ONLINE��#IMOS_DEV_STATUS_OFFLINE����imos_def.h�ж��� */
        public UInt32 ulIsOnline;

        /** �豸��չ״̬��Ԥ�� */
        public UInt32 ulDevExtStatus;

        /** Ԥ���ֶ���Ϣ */
        public RESERVED_INFO_S stReservedInfo;
    }


    /**
* @struct tagA8ScreenUnitInfo
* @brief A8��������Ԫ��Ϣ
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct A8_SCREEN_UNIT_INFO_S
    {
        /** ���������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szScreenName;

        /** ���������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szScreenCode;

        /** ״̬��Ϣ:����Ϊ#IMOS_DEV_STATUS_ONLINE������Ϊ#IMOS_DEV_STATUS_OFFLINE������޸�ʱ״̬��Ϣ������д */
        public UInt32 ulStatus;

        /** ������������Ŀ����ֵ���ڲ�ѯʱ��Ч */
        public UInt32 ulSplitScreenNum;

        /** ����������*/
        public UInt32 ulDialCode;

        /** ��չ״̬��Ϣ:0��ʾΪ��������1��ʾΪDC */
        public UInt32 ulExtStatus;
    }

    /**
* @struct tagTimerParam
* @brief ��ʱ���ػ�������Ϣ
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct TIMER_PARAM_S
    {
        /** ����ʱʹ�� */
        public UInt32 ulCountdownEnable;

        /** ��ʱ����ʹ�� */
        public UInt32 ulStartupEnable;

        /** ��ʱ�ػ�ʹ�� */
        public UInt32 ulShutdownEnable;

        /** ����ʱʱ�� */
        public UInt32 ulCountdownTime;

        /** ����ö����� */
        public UInt32 ulWeekDay;       /** #TV_WEEK_DAY_E */

        /** ʹ�ܼƻ���ʼʱ�� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_SIMPLE_TIME_LEN)]
        public byte[] szBeginTime;

        /** ʹ�ܼƻ�����ʱ�� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_SIMPLE_TIME_LEN)]
        public byte[] szEndTime;

    }

    /**
 * @struct tagTVWallBaseInfo
 * @brief ����ǽ��Ϣ��չ��Ϣ
 * @attention
 */
    [StructLayout(LayoutKind.Sequential)]
    public struct TVWALL_BASE_INFO_EXT_S
    {
        /** ����ǽ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szTVWallCode;

        /** ����ID */
        public UInt32 ulSceneId;

        /** ��ʱ���������Ϣ */
        public TIMER_PARAM_S stTimerParam;

        /** �Ƿ��� */
        public UInt32 ulAudioMute;

        /** �������  */
        public UInt32 ulAudioVolume;

    }


    /**
    * @struct tagTVWallBaseInfoA8
    * @brief ����ǽ��Ϣ
    * @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct TV_WALL_BASE_INFO_A8_S
    {
        /** ����ǽ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szTVWallName;

        /** ����ǽ����ID*/
        public UInt32 ulSceneId;

        /** ����ǽ���룬���ӵ���ǽʱ��дΪ����ƽ̨�Լ����ɣ����ӵ���ǽ�ɹ��󷵻�; ����д������д�ı������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szTVWallCode;

        /** ������֯����룬�����޸�ʱ��Ҫ��д��ƽ̨���ز������ֵ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szOrgCode;

        /** ����ǽ���� */
        public UInt32 ulTvWallRows;

        /** ����ǽ���� */
        public UInt32 ulTvWallColumns;

        /** �Ƿ����߿򲹳���0���ر� 1������ */
        public UInt32 ulCompEnable;

        /** ��ֱ������ȣ���λmm */
        public UInt32 ulVerOffset;

        /** ˮƽ������ȣ���λmm */
        public UInt32 ulHorOffset;

        /** �����ܶ� 0: ��ʾû������  ������ʾ��Ӧ��������4��ʾ 4*4 */
        public UInt32 ulMeshDensity;

        /** ����ǽ�ֱ��� */
        public UInt32 ulResolution;

        /** ����ǽ�������� */
        public UInt32 ulControlType;  /* #TV_WALL_CONTROL_TYPE_E */

        /** IP��ַ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szIPAddress;

        /** �˿ں� */
        public UInt32 ulPort;

        /** ʹ�ô��ں�*/
        public UInt32 ulComId;

        /** ʹ�ô���ͨ��Э��*/
        public UInt32 ulComProtocal;

        /** ��Ƶ�������*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szAudioInputCode;

        /** ��Ƶ�������*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szAudioOutputCode;

        /** ʱ���ӳ� */
        public UInt32 ulClockDelay;

        /** �����ź����� */
        public UInt32 ulInputType;

        /** �Ƿ����õ�ͼ */
        public UInt32 ulBasePicEnable;

        /** ��ͼID */
        public UInt32 ulBasePicId;

        /** ����ǽ��չ��Ϣ */
        public TVWALL_BASE_INFO_EXT_S stTVWallBaseInfoExt;

        /* �󶨼�������Ϣ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TV_WALL_SCREEN_MAX)]
        public A8_SCREEN_UNIT_INFO_S[] aulScreenInfo;

    }


    /**
    * @struct tagTVWallA8Info
    * @brief ����ǽ��Ϣ��Ϣ
    * @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct TV_WALL_A8_INFO
    {
        /** ����ǽ������Ϣ */
        public TV_WALL_BASE_INFO_A8_S stTVWallBaseInfo;

        /** ����ǽ�Ƿ����� */
        public UInt32 ulTVWallOnline;

    }


    /**
* @struct tagCameraUnitInfo
* @brief �������Ԫ��Ϣ
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct CAMERA_UNIT_INFO_S
    {
        /** ��������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szCamName;

        /** ��������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szCamCode;

        /** ״̬��Ϣ:����Ϊ#IMOS_DEV_STATUS_ONLINE������Ϊ#IMOS_DEV_STATUS_OFFLINE������޸�ʱ״̬��Ϣ������д */
        public UInt32 ulStatus;
    }


    /**
* @struct tagTVWallWindowInfo
* @brief ������Ϣ
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct TV_WALL_WINDOWS_INFO
    {
        /** ���ڱ��� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szWindowCode;

        /** ����ǽ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szTVWallCode;

        /** �������� #SCREEN_SPLIT_TYPE_E */
        public UInt32 ulSplitType;

        /** ����͸���� */
        public UInt32 ulTransparency;

        /** ���ڵ��Ӳ�� */
        public UInt32 ulLevel;

        /** ������λ�ã�����������Ϊ��λ���������Ͻ�Ϊԭ�� */
        public AREA_SCOPE_S stPosition;

        /** ��Ƶ����Դ��Ϣ�����û�ж�Ӧ����Ϊ�� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public CAMERA_UNIT_INFO_S[] stCamUnitInfo;


    }
    /**
* @struct tagCharContentInfo
* @brief LED������Ϣ
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct CHAR_CONTENT_INFO_S
    {
        /** ������λ�ã�����������Ϊ��λ���������Ͻ�Ϊԭ�� */
        public AREA_SCOPE_S stPosition;

        /** �������� */
        public UInt32 ulFonts;

        /** �����С */
        public UInt32 ulFontSize;

        /** ������ɫ */
        public UInt32 ulFontColor;

        /** �ּ�� */
        public UInt32 ulFontGap;

        /** �����Ƿ����� */
        public UInt32 ulContentEnable;

        /** LED���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        byte[] szContent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TVWALL_VIRTUAL_LED_INFO_S
    {
        /** ����ǽ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szTVWallCode;

        /** LED���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TV_WALL_LED_CONTENT_MAX)]
        public CHAR_CONTENT_INFO_S[] astContentInfo;

        /** ������λ�ã�����������Ϊ��λ���������Ͻ�Ϊԭ�� */
        AREA_SCOPE_S stPosition;

        /** �����߿� */
        public UInt32 ulBackgroundFrame;

        /** ������ɫ */
        public UInt32 ulBackgroundColor;

        /** �Ƿ����� */
        public UInt32 ulEnable;

        /** ����ID */
        public UInt32 ulSceneId;

    }


    /**
     * @struct tagTVWallBasePicInfo
     * @brief ����ǽ��ͼ��Ϣ
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct TVWALL_BASE_PIC_INFO_S
    {
        /** ����ǽ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szTVWallCode;

        /** ��ͼ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szBasePicName;

        /** ��ͼ·�� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szBasePicPath;

        /** ��ͼID */
        public UInt32 ulBasePicID;

        /** ����ID */
        public UInt32 ulSceneID;

        /** ������λ�ã�����������Ϊ��λ���������Ͻ�Ϊԭ�� */
        public AREA_SCOPE_S stPosition;

    }



    /**
* @struct tagTVWallAllInfo
* @brief ����ǽ��Ϣ
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct TV_WALL_ALL_INFO_S
    {
        /** �豸���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szDevCode;

        /** ����ǽ������Ϣ */
        TV_WALL_BASE_INFO_A8_S stBaseInfo;

        /** ��ѯ״̬ */
        public UInt32 ulTVWallPollStatus;

        /** �Ƿ��Զ����� */
        public UInt32 ulIsAutoAdsorbent;

        /** ���ڸ��� */
        public UInt32 ulWindowsNum;

        /** ��ͼ���� */
        public UInt32 ulBasePicNum;

        /** ������Ϣ */
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TV_WALL_SCREEN_MAX * IMOSSDK.IMOS_TV_WALL_SCREEN_WINDOWS_MAX+50)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public TV_WALL_WINDOWS_INFO[] stMosaicScreenInfo;

        /** LED��Ϣ */
        public TVWALL_VIRTUAL_LED_INFO_S stTVWallVirtualLEDInfo;

        /** ��ͼ��Ϣ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TV_WALL_BASE_PICTURE_MAX)]
        public TVWALL_BASE_PIC_INFO_S[] stTVWallBasePicInfo;
    }
    ;



    /**
      * @struct tagUserLoginIDInfo
      * @brief �û���¼ID��Ϣ�ṹ
      * @attention
      */
    [StructLayout(LayoutKind.Sequential)]
    public struct USER_LOGIN_ID_INFO_S
    {
        /** �û����� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_USER_CODE_LEN)]
        public byte[] szUserCode;

        /** �û���¼ID�����û���¼�����������ģ����Ǳ��һ���û���¼��Ψһ��ʶ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szUserLoginCode;

        /** �û���¼�Ŀͻ���IP��ַ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szUserIpAddress;
    }

    /**
    * @struct tagLoginInfo
    * @brief �û���¼��Ϣ�ṹ��
    * @attention ��
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct LOGIN_INFO_S
    {
        /** �û���¼ID��Ϣ */
        public USER_LOGIN_ID_INFO_S stUserLoginIDInfo;

        /** �û�������֯���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** �û����������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szDomainName;

        /** �û�����������, ȡֵΪ#MM_DOMAIN_SUBTYPE_LOCAL_PHYSICAL��#MM_DOMAIN_SUBTYPE_LOCAL_VIRTUAL */
        public UInt32 ulDomainType;
    }

    /**
    * @struct tagXpInfo
    * @brief XP��Ϣ�ṹ��
    * @attention ��
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct XP_INFO_S
    {
        /** XP���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szXpCode;

        /** ���� */
        public UInt32 ulScreenIndex;

        /** XP��һ������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szXpFirstWndCode;

        /** �����Խ����� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szVoiceTalkCode;

        /** �����㲥���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szVoiceBroadcastCode;

        /** SIPͨ�ŵ�ַ���ͣ�#IMOS_IPADDR_TYPE_IPV4ΪIPv4����; #IMOS_IPADDR_TYPE_IPV6ΪIPv6���� */
        public UInt32 ulSipAddrType;

        /** SIP������ͨ��IP��ַ������ʹ��XP��ʱ����Ч */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szSipIpAddress;

        /** SIP������ͨ�Ŷ˿ں� */
        public UInt32 ulSipPort;

        /** ������������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szServerCode;

    }

    /**
     * @struct tagQueryConditionItem
     * @brief ��ѯ������
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct QUERY_CONDITION_ITEM_S
    {
        /** ��ѯ��������: #QUERY_TYPE_E */
        public UInt32 ulQueryType;

        /** ��ѯ�����߼���ϵ����: #LOGIC_FLAG_E */
        public UInt32 ulLogicFlag;

        /** ��ѯ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_QUERY_DATA_MAX_LEN)]
        public byte[] szQueryData;
    }


    /**
     * @struct tagCommonQueryCondition
     * @brief ͨ�ò�ѯ����
     * @attention
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct COMMON_QUERY_CONDITION_S
    {
        /** ��ѯ���������в�ѯ������ʵ�ʸ���, ���ȡֵΪ#IMOS_QUERY_ITEM_MAX_NUM */
        public UInt32 ulItemNum;

        /** ��ѯ�������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_QUERY_ITEM_MAX_NUM)]
        public QUERY_CONDITION_ITEM_S[] astQueryConditionList;
    }

    /**
     * @struct tagQueryPageInfo
     * @brief ��ҳ������Ϣ
     * @brief ����ѯ���ݵ�ÿ���������Ӧһ����š���Ŵ�1��ʼ���������ӡ�
     * - ��ѯ���Ľ����ҳ����ʽ���أ�ÿ�β�ѯֻ�ܷ���һҳ��ҳ������������ulPageRowNum�趨����ΧΪ1~200��
     * - ÿ�β�ѯ�������ôӴ���ѯ�������ض���ţ�ulPageFirstRowNumber����ʼ
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct QUERY_PAGE_INFO_S
    {
        /** ��ҳ��ѯ��ÿҳ�������Ŀ��, ����Ϊ0, Ҳ���ܴ���#IMOS_PAGE_QUERY_ROW_MAX_NUM */
        public UInt32 ulPageRowNum;

        /** ��ҳ��ѯ�е�һ�����ݵ����(����ѯ�ӵ�ulPageFirstRowNumber�����ݿ�ʼ�ķ�������������), ȡֵ����ULONG���͵ķ�Χ���� */
        public UInt32 ulPageFirstRowNumber;

        /** �Ƿ��ѯ��Ŀ����, BOOL_TRUEʱ��ѯ; BOOL_FALSEʱ����ѯ */
        public UInt32 bQueryCount;
    }

    /**
     * @struct tagRspPageInfo
     * @brief ��ҳ��Ӧ��Ϣ
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct RSP_PAGE_INFO_S
    {
        /** ʵ�ʷ��ص���Ŀ�� */
        public UInt32 ulRowNum;

        /** ��������������Ŀ�� */
        public UInt32 ulTotalRowNum;
    }

    /**
     * @struct tagOrgResQueryItem
     * @brief ��֯�ڵ�����Դ��Ϣ��(��ѯ��Դ�б�ʱ����)
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct ORG_RES_QUERY_ITEM_S
    {
        /** ��Դ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szResCode;

        /** ��Դ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szResName;

        /** ��Դ���ͣ�ȡֵ��ΧΪ#IMOS_TYPE_E */
        public UInt32 ulResType;

        /** ��Դ������,Ŀǰ��Դ������ֻ�����������֯��Ч�������������Ϊ��̨/����̨;
            ����֯����Ϊ:1-��������2-�����������3-�����������. 4-�ϼ���������.
            5-�¼���������.6-ƽ����������. */
        public UInt32 ulResSubType;

        /** ��Դ״̬��Ŀǰֻ��������豸�����򣬶�������˵, ���ֶδ������ע��״̬��ȡֵΪ
            #IMOS_DEV_STATUS_ONLINE��#IMOS_DEV_STATUS_OFFLINE */
        public UInt32 ulResStatus;

        /** ��Դ����״̬���������豸��˵��ö��Ϊ#DEV_EXT_STATUS_E; ��������˵, ���ֶδ�������ע��״̬:
            ȡֵΪ#IMOS_DEV_STATUS_ONLINE��#IMOS_DEV_STATUS_OFFLINE */
        public UInt32 ulResExtStatus;

        /** ����Դ�Ƿ��Ǳ��������Դ, 1Ϊ���������Դ; 0Ϊ�ǻ������Դ */
        public UInt32 ulResIsBeShare;

        /** ��Դ������֯���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** ֧�ֵ�����Ŀ��������Դ����Ϊ�����ʱ��Ч��0:��Чֵ��1:������2:˫�� */
        public UInt32 ulStreamNum;

        /** �Ƿ�Ϊ������Դ��1Ϊ������Դ; 0Ϊ��������Դ */
        public UInt32 ulResIsForeign;

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct PRESET_INFO_S
    {
        /** Ԥ��λֵ, ȡֵ��ΧΪ#PTZ_PRESET_MINVALUE~�����������ļ������õ�Ԥ��λ���ֵ */
        public UInt32 ulPresetValue;

        /** Ԥ��λ����, ��Ҫ��д */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szPresetDesc;
    }

    /**
     * @struct tagPTZCtrlCommand
     * @brief ��̨����ָ��
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct PTZ_CTRL_COMMAND_S
    {
        /** ��̨������������, ȡֵΪ#MW_PTZ_CMD_E */
        public UInt32 ulPTZCmdID;

        /** ��̨����ת�� */
        public UInt32 ulPTZCmdPara1;

        /** ��̨������� */
        public UInt32 ulPTZCmdPara2;

        /** ��������Ĳ���ֵ,�����ֶ� */
        public UInt32 ulPTZCmdPara3;

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PLAY_WND_INFO_S
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szPlayWndCode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TIME_SLICE_S
    {
        /** ��ʼʱ�� ��ʽΪ"hh:mm:ss"��"YYYY-MM-DD hh:mm:ss", ��ʹ��������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
        public byte[] szBeginTime;

        /** ����ʱ�� ��ʽΪ"hh:mm:ss"��"YYYY-MM-DD hh:mm:ss", ��ʹ��������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
        public byte[] szEndTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct REC_QUERY_INFO_S
    {
        /** ����ͷ����*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szCamCode;

        /** ��������ʼ/����ʱ�� */
        public TIME_SLICE_S stQueryTimeSlice;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECORD_FILE_INFO_S
    {
        /** �ļ��� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_FILE_NAME_LEN)]
        public byte[] szFileName;

        /** �ļ���ʼʱ��, ����"%Y-%m-%d %H:%M:%S"��ʽ, �����޶�Ϊ24�ַ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
        public byte[] szStartTime;

        /** �ļ�����ʱ��, ����"%Y-%m-%d %H:%M:%S"��ʽ, �����޶�Ϊ24�ַ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
        public byte[] szEndTime;

        /** �ļ���С, Ŀǰ�ݲ�ʹ�� */
        public UInt32 ulSize;

        /** ������Ϣ, �ɲ��� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szSpec;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GET_URL_INFO_S
    {
        /** ��������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szCamCode;

        /** ¼���ļ��� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_FILE_NAME_LEN)]
        public byte[] szFileName;

        /** ¼�����ʼ/����ʱ��, ���е�ʱ���ʽΪ"YYYY-MM-DD hh:mm:ss" */
        public TIME_SLICE_S stRecTimeSlice;

        /** �ͻ���IP��ַ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szClientIp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VOD_SEVER_IPADDR_S
    {
        /** RTSP������IP��ַ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szServerIp;

        /** RTSP�������˿� */
        public UInt16 usServerPort;

        /** ����λ, �����ֽڶ���, ��ʵ�ʺ��� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] acReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct URL_INFO_S
    {
        /** URL��ַ*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IE_URL_LEN)]
        public byte[] szURL;

        /** �㲥��������IP��ַ�Ͷ˿� */
        public VOD_SEVER_IPADDR_S stVodSeverIP;

        /** ���������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] szDecoderTag;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EC_INFO_S
    {
        /** EC����, EC��Ψһ��ʶ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szECCode;

        /** EC���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szECName;

        /** EC���ͣ�ȡֵΪ#IMOS_DEVICE_TYPE_E, �Ϸ�ȡֵ�μ�#ulChannum������˵�� */
        public UInt32 ulECType;

        /** ECͨ������:
            ���ֳ���EC���Ͷ�Ӧ��ͨ����������:
            EC1101(#IMOS_DT_EC1101_HF)/EC1001(#IMOS_DT_EC1001)/EC1001-HF(#IMOS_DT_EC1001_HF): 1
            EC1501(#IMOS_DT_EC1501_HF)/R1000 (#IMOS_DT_R1000) : 1
            EC2004(#IMOS_DT_EC2004_HF)/VR2004(#IMOS_DT_VR2004): 4
            EC1102(#IMOS_DT_EC1102_HF)/VR1102(#IMOS_DT_VR1102): 2
            EC1801(#IMOS_DT_EC1801_HH): 1
            EC2016(#IMOS_DT_EC2016_HC): 16
            EC2016[8CH](#IMOS_DT_EC2016_HC_8CH): 8
            EC2016[4CH](#IMOS_DT_EC2016_HC_4CH): 4
            HIC5201-H(#IMOS_DT_HIC5201)/HIC5221-H(#IMOS_DT_HIC5221): 1
        */
        public UInt32 ulChannum;

        /** �Ƿ�֧���鲥, 1Ϊ֧��; 0Ϊ��֧�� */
        public UInt32 ulIsMulticast;

        /** ���¸澯�¶�����, ȡֵΪ-100~49 */
        public Int32 lTemperatureMax;

        /** ���¸澯�¶�����, ȡֵΪ50~100 */
        public Int32 lTemperatureMin;

        /** �澯ʹ��, 1Ϊʹ��; 0Ϊ��ʹ�� */
        public UInt32 ulEnableAlarm;

        /** EC������֯���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** ʱ��ͬ����ʽ��Ĭ��Ϊ1����ʾʹ��H3C��˽��ͬ����ʽ��2��ʾNTP��ͬ����ʽ */
        public UInt32 ulTimeSyncMode;

        /** ʱ��, ȡֵΪ-12~12 */
        public Int32 lTimeZone;

        /** �������ã������ķ����������ã�ȡֵΪ:#TD_LANGUAGE_E */
        public UInt32 ulLanguage;

        /** �Ƿ����ñ��ػ��棬1��ʾ����; 0��ʾ��������Ĭ��ֵΪ0 */
        public UInt32 ulEnableLocalCache;

        /** ���ײ�, ȡֵΪ:#IMOS_STREAM_RELATION_SET_E
            0��MPEG4+MPEG4(#IMOS_SR_MPEG4_MPEG4)
            1��H264������(#IMOS_SR_H264_SHARE)
            2��MPEG2+MPEG4(#IMOS_SR_MPEG2_MPEG4)
            3��H264+MJPEG(#IMOS_SR_H264_MJPEG)
            4��MPEG4������(#IMOS_SR_MPEG4_SHARE)
            5��MPEG2������(#IMOS_SR_MPEG2_SHARE)
            8: MPEG4������_D1(#IMOS_SR_STREAM_MPEG4_8D1)
            9��MPEG2+MPEG2(#IMOS_SR_MPEG2_MPEG2)
            11��H264+H264(#IMOS_SR_H264_H264)
        */
        public UInt32 ulEncodeSet;

        /** ��ʽ, ȡֵΪ#IMOS_PICTURE_FORMAT_E */
        public UInt32 ulStandard;

        /** ��Ƶ����Դ��ȡֵΪ#IMOS_AUDIO_INPUT_SOURCE_E */
        public UInt32 ulAudioinSource;

        /** �����Խ���Դ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szAudioCommCode;

        /** �����㲥��Դ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szAudioBroadcastCode;

        /** �豸�������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN)]
        public byte[] szDevPasswd;

        /** �豸����, Ŀǰ���ֶ�δʹ�� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szDevDesc;

        /** EC��IP��ַ, ��Ӽ��޸�EC������д�ò���, ��ѯEC��Ϣʱ���ظ��ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szECIPAddr;

        /** EC������״̬,��Ӽ��޸�EC������д�ò���, ��ѯEC��Ϣʱ���ظ��ֶ�, 1Ϊ����; 0Ϊ���� */
        public UInt32 ulIsECOnline;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
        public byte[] szReserve;

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EC_QUERY_ITEM_S
    {
        /** EC���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szECCode;

        /** EC���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szECName;

        /** EC���ͣ�ȡֵΪ#IMOS_DEVICE_TYPE_E */
        public UInt32 ulECType;

        /** �豸��ַ���ͣ�1-IPv4 2-IPv6 */
        public UInt32 ulDevaddrtype;

        /** �豸��ַ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szDevAddr;

        /** ������֯���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** �豸�Ƿ�����ȡֵΪ#IMOS_DEV_STATUS_ONLINE��#IMOS_DEV_STATUS_OFFLINE����imos_def.h�ж��� */
        public UInt32 ulIsOnline;

        /** �豸��չ״̬��ȡֵΪ#DEV_EXT_STATUS_E */
        public UInt32 ulDevExtStatus;

        /** �Ƿ�֧���鲥, 1Ϊ֧���鲥; 0Ϊ��֧���鲥 */
        public UInt32 ulIsMulticast;

        /** �澯ʹ��, 1Ϊʹ�ܸ澯; 0Ϊ��ʹ�ܸ澯 */
        public UInt32 ulEnableAlarm;

        /** ���ײ����ͣ�ȡֵΪ#IMOS_STREAM_RELATION_SET_E */
        public UInt32 ulEncodeType;

        /** ��ʽ��ȡֵΪ#IMOS_PICTURE_FORMAT_E */
        public UInt32 ulStandard;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DC_INFO_S
    {
        /** DC���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szDCCode;

        /** DC���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szDCName;

        /** DC����, ȡֵΪ#IMOS_DEVICE_TYPE_E, �Ϸ�ȡֵ�μ�#ulChannum������˵�� */
        public UInt32 ulDCType;

        /** DCͨ������:
            ���ֳ���DC���Ͷ�Ӧ��ͨ����������:
            DC1001(#IMOS_DT_DC1001): 1
            DC2004(#IMOS_DT_DC2004_FF)/VL2004(#IMOS_DT_VL2004): 4
            DC1801(#IMOS_DT_DC1801_FH): 1
        */
        public UInt32 ulChannum;

        /** �Ƿ�֧���鲥, 1Ϊ֧���鲥; 0Ϊ��֧���鲥 */
        public UInt32 ulIsMulticast;

        /** ���¸澯�¶�����, ȡֵΪ-100~49 */
        public Int32 lTemperatureMax;

        /** ���¸澯�¶�����, ȡֵΪ50~100 */
        public Int32 lTemperatureMin;

        /** �澯ʹ��, 1Ϊʹ�ܸ澯; 0Ϊ��ʹ�ܸ澯 */
        public UInt32 ulEnableAlarm;

        /** ������֯���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** ʱ��ͬ����ʽ��Ĭ��Ϊ1����ʾʹ��H3C��˽��ͬ����ʽ��2��ʾNTP��ͬ����ʽ */
        public UInt32 ulTimeSyncMode;

        /** ʱ��, ȡֵΪ-12~12 */
        public Int32 lTimeZone;

        /** �������ã������ķ����������ã�ȡֵΪ:#TD_LANGUAGE_E */
        public UInt32 ulLanguage;

        /** ��ʽ, ȡֵΪ#IMOS_PICTURE_FORMAT_E */
        public UInt32 ulStandard;

        /** ���ײͣ�ȡֵΪ#IMOS_STREAM_RELATION_SET_E
            ����Ϊ���������ײ�ֵ��
            1��H264(#IMOS_SR_H264_SHARE)
            3: MJPEG(#IMOS_SR_H264_MJPEG)
            4��MEPG4(#IMOS_SR_MPEG4_SHARE)
            5��MEPG2(#IMOS_SR_MPEG2_SHARE)
        */
        public UInt32 ulEncodeSet;

        /** �豸�������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN)]
        public byte[] szDevPasswd;

        /** �豸����, Ŀǰ���ֶ�δʹ�� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szDevDesc;

        /** DC��IP��ַ,��Ӽ��޸�DC������д�ò���,��ѯDC��Ϣʱ�᷵�ظ��ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szDCIPAddr;

        /** EC������״̬,��Ӽ��޸�EC������д�ò���, ��ѯEC��Ϣʱ���ظ��ֶ�, 1Ϊ����; 0Ϊ���� */
        public UInt32 ulIsDCOnline;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DC_QUERY_ITEM_S
    {
        /** DC���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szDCCode;

        /** DC���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szDCName;

        /** DC���ͣ�ȡֵΪ#IMOS_DEVICE_TYPE_E */
        public UInt32 ulDCType;

        /** DC�豸��ַ���ͣ�1-IPv4 2-IPv6 */
        public UInt32 ulDevaddrtype;

        /** DC�豸��ַ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szDevAddr;

        /** DC������֯���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** �豸�Ƿ�����, ȡֵΪ#IMOS_DEV_STATUS_ONLINE��#IMOS_DEV_STATUS_OFFLINE����imos_def.h�ж��� */
        public UInt32 ulIsOnline;

        /** �豸��չ״̬��ö��ֵΪ#DEV_EXT_STATUS_E */
        public UInt32 ulDevExtStatus;

        /** �Ƿ�֧���鲥, 1Ϊ֧���鲥; 0Ϊ��֧���鲥 */
        public UInt32 ulIsMulticast;

        /** �澯ʹ��, 1Ϊʹ�ܸ澯; 0Ϊ��ʹ�ܸ澯 */
        public UInt32 ulEnableAlarm;

        /** ���ײ����ͣ�ȡֵΪ#IMOS_STREAM_RELATION_SET_E */
        public UInt32 ulEncodeType;

        /** ��ʽ, ȡֵΪ#IMOS_PICTURE_FORMAT_E */
        public UInt32 ulStandard;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CAMERA_INFO_S
    {
        /** ��������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szCameraCode;

        /** ��������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szCameraName;

        /** ���������, ȡֵΪ#CAMERA_TYPE_E */
        public UInt32 ulCameraType;

        /** ���������, �ɲ��� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szCameraDesc;

        /** ��̨����Э��, Ŀǰ֧�ֵİ���:PELCO-D, PELCO-P, ALEC, VISCA, ALEC_PELCO-D, ALEC_PELCO-P, MINKING_PELCO-D, MINKING_PELCO-P */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szPtzProtocol;

        /** ��̨��ַ��, ȡֵΪ0~255, ����ȡֵ����̨�������ʵ�ʵ�ַ����� */
        public UInt32 ulPtzAddrCode;

        /** ��̨Э�鷭��ģʽ,Ŀǰֻ����дΪ#PTZ_TRANSLATE_EP(�ն˷���ģʽ) */
        public UInt32 ulPtzTranslateMode;

        /** ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szLongitude;

        /** γ�� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szLatitude;

        /** ����λ�����趨��Ԥ��λ�ı�����Ӧ */
        public UInt32 ulGuardPosition;

        /** �Զ�����ʱ��, ��λΪ��, ��󲻳���3600��, 0��ʾ������ */
        public UInt32 ulAutoGuard;

        /** �豸����, �ɲ��� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szDevDesc;

        /** EC���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szECCode;

        /** EC��IP��ַ,�ڰ󶨼��޸�Cameraʱ,������д,��ѯCamera��Ϣʱ�᷵�ظ��ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szECIPAddr;

        /** ����ECͨ��������, �Ӿ���������� */
        public UInt32 ulChannelIndex;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }


    /**
* @struct tagXpStreamInfo
* @brief XPʵʱ�������Ϣ�ṹ
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct XP_STREAM_INFO_S
    {
        /** ֧�ֵĵ��鲥���ͣ�0Ϊ��֧�ֵ�����1Ϊ��֧�ֵ���Ҳ֧���鲥 */
        public UInt32 ulStreamType;

        /** ֧�ֵ�������Э�� �μ�#IMOS_TRANS_TYPE_E��ĿǰXPֻ֧������Ӧ��TCP */
        public UInt32 ulStreamTransProtocol;

        /** ֧�ֵ������䷽ʽ �μ�#IMOS_STREAM_SERVER_MODE_E��ĿǰXPֻ֧������Ӧ��ֱ������ */
        public UInt32 ulStreamServerMode;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VIN_CHANNEL_S
    {
        /** ��Ƶ����ͨ������ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szVinChannelDesc;

        /** �鲥��ַ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szMulticastAddr;

        /** �鲥�˿�,��ΧΪ��10002-65534���ұ���Ϊż�� */
        public UInt32 ulMulticastPort;

        /** MSѡ�����Ӧ����, 1Ϊ����Ӧ; 0Ϊ������Ӧ */
        public UInt32 ulIsAutofit;

        /** ʹ��MS��Ŀ, ��ʵ���������, ����Ӧ����#ulIsAutofitΪ����Ӧ, ��ֵΪ0;
            ����Ӧ����#ulIsAutofitΪ������Ӧ(��ָ��), ��ֵΪ1 */
        public UInt32 ulUseMSNum;

        /** MS�����б� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (IMOSSDK.IMOS_MS_MAX_NUM_PER_CHANNEL * IMOSSDK.IMOS_DEVICE_CODE_LEN))]
        public byte[,] aszMSCode;

        /** �Ƿ�����ͼ���ڵ����澯, 1Ϊ����; 0Ϊ������ */
        public UInt32 ulEnableKeepout;

        /** �Ƿ������˶����澯, 1Ϊ�����˶����澯; 0Ϊ�������˶����澯 */
        public UInt32 ulEnableMotionDetect;

        /** �Ƿ�������Ƶ��ʧ�澯, 1Ϊ������Ƶ��ʧ�澯; 0Ϊ��������Ƶ��ʧ�澯 */
        public UInt32 ulEnableVideoLost;

        /** �󶨵Ĵ��ڱ�ţ���������д0 */
        public UInt32 ulSerialIndex;

        /** ���ȣ�ȡֵΪ��0~255�� */
        public UInt32 ulBrightness;

        /** �Աȶȣ�ȡֵΪ��0~255�� */
        public UInt32 ulContrast;

        /** ���Ͷȣ�ȡֵΪ��0~255�� */
        public UInt32 ulSaturation;

        /** ɫ����ȡֵΪ��0~255�� */
        public UInt32 ulTone;

        /** �Ƿ���������, 1Ϊ��������; 0Ϊ���������� */
        public UInt32 ulAudioEnabled;

        /** ��Ƶ����, ȡֵΪ#IMOS_AUDIO_FORMAT_E */
        public UInt32 ulAudioCoding;

        /** ��Ƶ����, ȡֵΪ#IMOS_AUDIO_CHANNEL_TYPE_E */
        public UInt32 ulAudioTrack;

        /** ��Ƶ������, ȡֵΪ#IMOS_AUDIO_SAMPLING_E */
        public UInt32 ulSamplingRate;

        /** ��Ƶ����, �������� */
        public UInt32 ulAudioCodeRate;

        /** ��Ƶ����ֵ��ȡֵΪ��0~255�� */
        public UInt32 ulIncrement;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VIDEO_STREAM_S
    {
        /** ��������, ȡֵΪ#IMOS_STREAM_TYPE_E, Ŀǰ��֧��#IMOS_ST_TS */
        public UInt32 ulStreamType;

        /** ��������1Ϊ������2Ϊ���� */
        public UInt32 ulStreamIndex;

        /** ��ʹ�ܱ�ʶ, 1Ϊʹ��; 0Ϊ��ʹ�� */
        public UInt32 ulEnabledFlag;

        /** �����䷽ʽ, ȡֵΪ#IMOS_TRANS_TYPE_E */
        public UInt32 ulTranType;

        /** �����ʽ, ȡ���ھ�������ײ�ֵ, ȡֵΪ#IMOS_VIDEO_FORMAT_E */
        public UInt32 ulEncodeFormat;

        /** �ֱ���, ȡֵΪ#IMOS_PICTURE_SIZE_E */
        public UInt32 ulResolution;

        /** ���� */
        public UInt32 ulBitRate;

        /** ֡��,��ȡ��ֵ��1, 3, 5, 8, 10, 15, 20, 25, 30 */
        public UInt32 ulFrameRate;

        /** GOPģʽ, ȡֵΪ#IMOS_GOP_TYPE_E */
        public UInt32 ulGopMode;

        /** I֡���, ȡ����GOPģʽֵ, ��GOPģʽΪ#IMOS_GT_Iʱ, I֡���Ϊ1; ��GOPģʽΪ#IMOS_GT_IPʱ, I֡���Ϊ10~50 */
        public UInt32 ulIFrameInterval;

        /** ͼ������, ȡֵΪ#IMOS_VIDEO_QUALITY_E */
        public UInt32 ulImageQuality;

        /** ������ģʽ, ȡֵΪ#IMOS_ENC_MODE_E */
        public UInt32 ulEncodeMode;

        /** ���ȼ�, ���ڱ���ģʽΪ#IMOS_EM_CBRʱ�����ø�ֵ, ȡֵΪ#IMOS_CBR_ENC_MODE_E */
        public UInt32 ulPriority;

        /** ����ƽ����ȡֵΪ#IMOS_STREAM_SMOOTH_E */
        public UInt32 ulSmoothValue;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AREA_SCOPE_S
    {
        /** ���Ͻ�x����, ȡֵΪ0~100 */
        public UInt32 ulTopLeftX;

        /** ���Ͻ�y����, ȡֵΪ0~100 */
        public UInt32 ulTopLeftY;

        /** ���½�x����, ȡֵΪ0~100 */
        public UInt32 ulBottomRightX;

        /** ���½�y����, ȡֵΪ0~100 */
        public UInt32 ulBottomRightY;

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VIDEO_AREA_S
    {
        /** ��������, ȡֵΪ1~4 */
        public UInt32 ulAreaIndex;

        /** �Ƿ�ʹ��, 1Ϊʹ��; 0Ϊ��ʹ�� */
        public UInt32 ulEnabledFlag;

        /** ������, 1��5����1����������ߡ���ֵ�����˶����������Ч */
        public UInt32 ulSensitivity;

        /** �������� */
        public AREA_SCOPE_S stAreaScope;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DETECT_AREA_S
    {
        /** �ڵ�������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DETECT_AREA_MAXNUM)]
        public VIDEO_AREA_S[] astCoverDetecArea;

        /** �˶�������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DETECT_AREA_MAXNUM)]
        public VIDEO_AREA_S[] astMotionDetecArea;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEV_CHANNEL_INDEX_S
    {
        /** �豸���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szDevCode;

        /** �豸���ͣ����豸Ϊ������ʱ, ȡֵΪ#IMOS_TYPE_EC; ���豸Ϊ������ʱ, ȡֵΪ#IMOS_TYPE_DC */
        public UInt32 ulDevType;

        /** ͨ�������ţ���Ϊ:��Ƶ��Ƶͨ��������ͨ����������ͨ��(����/���), ȡֵ�Ӿ�������� */
        public UInt32 ulChannelIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SCREEN_INFO_S
    {
        /** ���������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szScreenCode;

        /** ���������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szScreenName;

        /** ����������, �ɲ��� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szDevDesc;

        /**  DC��IP��ַ, �ڰ󶨼��޸�Screenʱ, ������д; ��ѯScreen��Ϣʱ�᷵�ظ��ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szDCIPAddr;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VOUT_CHANNEL_S
    {
        /** �߼����ͨ������, ȡֵΪ1~#IMOS_DC_LOGIC_CHANNEL_MAXNUM */
        public UInt32 ulVoutChannelindex;

        /** �߼����ͨ������, �ɲ��� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szVoutChannelDesc;

        /** �Ƿ�ʹ��, 1Ϊʹ��; 0Ϊ��ʹ�� */
        public UInt32 ulEnable;

        /** ��������, ȡֵΪ#IMOS_STREAM_TYPE_E, Ŀǰ��֧��#IMOS_ST_TS */
        public UInt32 ulStreamType;

        /** ������ģʽ, ȡֵΪ#IMOS_TRANS_TYPE_E */
        public UInt32 ulTranType;

        /** �Ƿ�����������, 1Ϊ����; 0Ϊ������ */
        public UInt32 ulEnableJitterBuff;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OSD_TIME_S
    {
        /** ʱ��OSD����, �̶�Ϊ1 */
        public UInt32 ulOsdTimeIndex;

        /** ʱ��OSDʹ��, 1Ϊʹ��; 0Ϊ��ʹ�� */
        public UInt32 ulEnableFlag;

        /** ʱ��OSDʱ���ʽ */
        public UInt32 ulOsdTimeFormat;

        /** ʱ��OSD���ڸ�ʽ */
        public UInt32 ulOsdDateFormat;

        /** ʱ��OSD��ɫ, ȡֵΪ#IMOS_OSD_COLOR_E */
        public UInt32 ulOsdColor;

        /** ʱ��OSD͸����, ȡֵΪ#IMOS_OSD_ALPHA_E */
        public UInt32 ulTransparence;

        /** ʱ��OSD�������� */
        public AREA_SCOPE_S stAreaScope;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OSD_NAME_S
    {
        /** �Ƿ�ʹ�ܳ���OSD, 1Ϊʹ��; 0Ϊ��ʹ�� */
        public UInt32 ulEnabledFlag;

        /** ����OSD����, �̶�Ϊ1 */
        public UInt32 ulOsdNameIndex;

        /** ����OSD��ɫ, ȡֵΪ#IMOS_OSD_COLOR_E */
        public UInt32 ulOsdColor;

        /** ����OSD͸����, ȡֵΪ#IMOS_OSD_ALPHA_E */
        public UInt32 ulTransparence;

        /** ����OSD�������� */
        public AREA_SCOPE_S stAreaScope;

        /** ��һ��(��)����OSD����, ȡֵΪ#IMOS_INFO_OSD_TYPE_E */
        public UInt32 ulOsdType1;

        /** ��һ��(��)����OSD���ݣ������֣���ֵΪ�ַ������Ϊ20�ַ�����ͼƬ����ֵΪOSDͼƬ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szOsdString1;

        /** �ڶ���(��)����OSD����, ȡֵΪ#IMOS_INFO_OSD_TYPE_E */
        public UInt32 ulOsdType2;

        /** �ڶ���(��)����OSD���ݣ������֣���ֵΪ�ַ������Ϊ20�ַ�����ͼƬ����ֵΪOSDͼƬ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szOsdString2;

        /** (��һ���͵ڶ���)����OSD֮����л�ʱ��, ��λΪ��, ȡֵΪ0~300��ȡֵΪ0, ��ʾֻ��ʾ��һ��(��)OSD */
        public UInt32 ulSwitchIntval;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] szReserve;
    }

    /// <summary>
    /// �澯����Ϣ�ṹ��
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AS_ALARMPUSH_UI_S
    {
        /// <summary>
        /// �澯�¼�����
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] byAlarmEventCode;

        /// <summary>
        /// �澯Դ����
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] byAlarmSrcCode;

        /// <summary>
        /// �澯Դ����
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] byAlarmSrcName;

        /// <summary>
        /// ʹ�ܺ�����
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] byActiveName;

        /// <summary>
        /// �澯���� ALARM_TYPE_E ��sdk_def.h�ж���
        /// </summary>
        public UInt32 ulAlarmType;

        /// <summary>
        /// �澯���� ALARM_SEVERITY_LEVEL_E ��sdk_def.h�ж���
        /// </summary>
        public UInt32 ulAlarmLevel;

        /// <summary>
        /// �澯����ʱ��
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
        public byte[] byAlarmTime;

        /// <summary>
        /// �澯������Ϣ
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] byAlarmDesc;

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct OSD_MASK_AREA_S
    {
        /** �ڸ����� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_MASK_AREA_MAXNUM)]
        public VIDEO_AREA_S[] astMaskArea;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OSD_INFO_S
    {
        /** ʱ��OSD */
        public OSD_TIME_S stOSDTime;

        /** ����OSD */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_OSD_NAME_MAXNUM)]
        public OSD_NAME_S[] astOSDName;

        /** �ڸ����� */
        public OSD_MASK_AREA_S stOSDMaskArea;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PHYOUT_CHANNEL_S
    {
        /** ����ģʽ��ȡֵΪ1��4������BNC�ڵķ����� */
        public UInt32 ulPhyoutMode;

        /** ��Ƶ�����ʽ��ȡֵΪ#IMOS_VIDEO_FORMAT_E */
        public UInt32 ulDecodeFormat;

        /** ��Ƶ��ʽ��ȡֵΪ#IMOS_AUDIO_FORMAT_E */
        public UInt32 ulAudioFormat;

        /** �������ã�ȡֵΪ#IMOS_AUDIO_CHANNEL_TYPE_E */
        public UInt32 ulAudioTrack;

        /** �Ƿ�������������, 1Ϊ����; 0Ϊ������ */
        public UInt32 ulAudioEnabled;

        /** �������, ȡֵΪ1~7 */
        public UInt32 ulVolume;

        /** ��Ƶ���ѡ��, �ӹ���ģʽ����#ulPhyoutMode�������������ģʽȡֵΪ1, ���ֵΪ1; �������ģʽȡֵΪ4, ���ֵȡֵΪ1~4 */
        public UInt32 ulOutputIndex;

        /** ������������, ��ʾ������ͨ�����ɰ󶨵ļ���������, Ŀǰ�̶�Ϊ1 */
        public UInt32 ulMaxScreenNum;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VINCHNL_BIND_CAMERA_S
    {
        /** �豸ͨ��������Ϣ */
        public DEV_CHANNEL_INDEX_S stECChannelIndex;

        /** �������Ϣ */
        public CAMERA_INFO_S stCameraInfo;

        /** ��Ƶ����ͨ����Ϣ */
        public VIN_CHANNEL_S stVinChannel;

        /** OSD��Ϣ */
        public OSD_INFO_S stOSDInfo;

        /** ��Ƶ����������Ƶ����ʵ����Ŀ, ���ȡֵΪ#IMOS_STREAM_MAXNUM, �Ӿ������ײ�ֵ���� */
        public UInt32 ulVideoStreamNum;

        /** ��Ƶ����Ϣ���� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_STREAM_MAXNUM)]
        public VIDEO_STREAM_S[] astVideoStream;

        /** ������򣬰����˶�����Լ��ڵ�������� */
        public DETECT_AREA_S stDetectArea;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct VOUTCHNL_BIND_SCREEN_S
    {
        /** �豸ͨ��������Ϣ */
        public DEV_CHANNEL_INDEX_S stDCChannelIndex;

        /** ��������Ϣ */
        public SCREEN_INFO_S stScreenInfo;

        /** �߼����ͨ����Ϣ */
        public VOUT_CHANNEL_S stVoutChannel;

        /** OSD��Ϣ */
        public OSD_INFO_S stOSDInfo;

        /** �������ͨ����Ϣ */
        public PHYOUT_CHANNEL_S stPhyoutChannel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct XP_RUN_INFO_EX_S
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public Byte[] szPortCode;     /**< ͨ����Դ���� */

        public UInt32 ulErrCode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CAM_AND_CHANNEL_QUERY_ITEM_S
    {
        /** �豸ͨ��������Ϣ */
        public DEV_CHANNEL_INDEX_S stECChannelIndex;

        /** ��������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szCamCode;

        /** ��������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szCamName;

        /** ���������, ȡֵΪ#CAMERA_TYPE_E */
        public UInt32 ulCamType;

        /** ��̨����Э�� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szPtzProtocol;

        /** ��̨��ַ��, ȡֵΪ0~255, ����ȡֵ����̨�������ʵ�ʵ�ַ����� */
        public UInt32 ulPtzAddrCode;

        /** �鲥��ַ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szMulticastAddr;

        /** �鲥�˿�, ��ΧΪ��10002-65534 */
        public UInt32 ulMulticastPort;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SCR_AND_CHANNEL_QUERY_ITEM_S
    {
        /** �豸ͨ��������Ϣ */
        public DEV_CHANNEL_INDEX_S stDCChannelIndex;

        /** ���������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szScrCode;

        /** ���������� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szScrName;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SPLIT_SCR_INFO_S
    {
        /** ����ģʽ,ȡֵΪ#SPLIT_SCR_MODE_E */
        public UInt32 ulSplitScrMode;

        /** ��������(ȫ��ʱ��Ч) */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szSplitScrCode;

        /** �Ƿ�"�Զ��л�������"(#BOOL_TRUE ��,#BOOL_FALSE ��)  */
        public UInt32 bSwitchStream;

        /** Ԥ���ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RES_ITEM_V2_S
    {
        /** V1��Դ��Ϣ�� */
        public ORG_RES_QUERY_ITEM_S stResItemV1;

        /** ��Դ������֯������ */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szOrgName;

        /** ��Դ������Ϣ��������Դ�����������ʱ��ȡֵΪ#CAMERA_ATTRIBUTE_E��������Դ���͸��ֶ���δʹ�� */
        public UInt32 ulResAttribute;

        /** �����ECR HFϵ�е���������߼��������ڵ��豸�����ײͣ�
            ������Դ����,����ͨ�ò�ѯ����IS_QUERY_ENCODESETû����д, ��������"����ѯ", ���ֶξ�Ϊ��Чֵ#IMOS_SR_INVALID
            ȡֵΪ#IMOS_STREAM_RELATION_SET_E */
        public UInt32 ulDevEncodeSet;

        /** �����ֶ� */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 184)]
        public byte[] szReserve;

        public static RES_ITEM_V2_S GetInstance(ORG_RES_QUERY_ITEM_S stRes)
        {
            RES_ITEM_V2_S stRes_V2 = new RES_ITEM_V2_S();
            stRes_V2.stResItemV1 = stRes;
            stRes_V2.szOrgName = new byte[IMOSSDK.IMOS_NAME_LEN];
            stRes_V2.szReserve = new byte[184];
            stRes_V2.ulDevEncodeSet = 0;
            stRes_V2.ulResAttribute = 0;
            return stRes_V2;
        }
    }
    public struct XP_RECT_S
    {
        public int lLeft;

        public int lTop;

        public int lRight;

        public int lBottom;
    }

    public class IMOSSDK
    {


        /*@brief ��չ�ֶγ��� */
        public const int IMOS_RESERVED_LEN = 64;

        /** A8�豸ͨ��������� */
        public const int A8_CHANNEL_MAX_NUM = 32;
        /** ����A8�豸����ǽ������� */
        public const int A8_TV_WALL_MAX_NUM = 8;
        /** ��������ǽ�����ʾ��(ƴ����)�� */
        public const int IMOS_TV_WALL_SCREEN_MAX = 128;
        /** @brief A�豸���ͨ�������� */
        public const int A_CHANNEL_MAX_NUM = 512;

        /** @brief A�豸ͨ���ż�������󳤶� */
        public const int A_CHANNEL_INDEX_TYPE_LEN = 15;


        /** �����������ʾ���ڸ��� */
        public const int IMOS_TV_WALL_SCREEN_WINDOWS_MAX = 4;

        /** ��������ǽLED �������*/
        public const int IMOS_TV_WALL_LED_CONTENT_MAX = 4;

        /** ��������ǽ��󴰿ڸ��� */
        public const int IMOS_TV_WALL_WINDOWS_MAX = (IMOS_TV_WALL_SCREEN_MAX * IMOS_TV_WALL_SCREEN_WINDOWS_MAX);

        /** ��������ǽ����ϴ���ͼ�� */
        public const int IMOS_TV_WALL_BASE_PICTURE_MAX = 8;


        /*@brief imos_simple_time ʱ����Ϣ�ַ������� "hh:mm:ss" */
        public const int IMOS_SIMPLE_TIME_LEN = 12;
        /*@brief imos_simple_date ������Ϣ�ַ������� "YYYY-MM-DD"*/
        public const int IMOS_SIMPLE_DATE_LEN = 12;
        /** A8 EDID�ļ�����С */
        public const int A8_EDID_MAX_SIZE = 256;
        /** Ĭ�ϵ���ǽ����ID */
        public const int CS_TVWALL_SCENE_ID = 99;
        /** ����ǽ����������� */
        public const int TVWALL_SCENE_MAX_NUM = 32;


        /** ����ǽ����Ĭ��ͷ */
        public const string TVWALL_DEFAULT_CODE = "tvwall";

        /** ����ǽ���ڱ���Ĭ��ͷ */
        public const string WINDOW_DEFAULT_CODE = "window";

        /** ����ǽ���������Ĭ��ͷ */
        public const string CAMERA_DEFAULT_CODE = "cam";

        /** ����ǽ����������Ĭ��ͷ */
        public const string SCREEN_DEFAULT_CODE = "screen";

        /** ����ǽ���������Ĭ��ͷ */
        public const string TVWALL_SCENE_GROUP_DEFAULT_CODE = "scenegroup";

        /** ����ǽ�����ñ��� */
        public const string A8_TVWALL_DISABLE_CODE = "DISABLE_TVWALL_CODE";

        /** ����ǽ�����������ñ��� */
        public const string A8_INVILD_SCREEN_CODE = "INVALID_SCREEN_CODE";

        /** ����ǽ����������ñ��� */
        public const string A8_INVILD_CAMERA_CODE = "INVALID_CAMERA_CODE";

        /* End added by y01359 2014-10-09 for A8 */

        /** @brief ѡ��ֵ�ڴ�鳤����Сֵ */
        public const int IMOS_OPTION_BUFFER_LEN_MIN = 1;

        /** @brief ѡ��ֵ�ڴ�鳤�����ֵ */
        public const int IMOS_OPTION_BUFFER_LEN_MAX = 64;


        /** @brief һ������ǽ�����������������Ŀ */
        public const int IMOS_MONITOR_MAXNUM_PER_WALL = 256;


        /** @brief һ������ǽ�����ķ����������Ŀ */
        public const int IMOS_SPLIT_MAXNUM_PER_WALL = ((int)SPLIT_SCR_MODE_E.SPLIT_SCR_MODE_MAX * IMOS_MONITOR_MAXNUM_PER_WALL);


        public const int IMOS_NAME_LEN = 64;


        public const int IMOS_PHONE_LEN = 64;

        public const int IMOS_CODE_LEN = 48;

        public const int IMOS_IPADDR_LEN = 64;

        public const int IMOS_STRING_LEN_256 = 256;

        /*@brief ��Դ������Ϣ�ַ�������*/
        public const int IMOS_RES_CODE_LEN = IMOS_CODE_LEN;

        /*@brief �豸������Ϣ�ַ�������*/
        public const int IMOS_DEVICE_CODE_LEN = IMOS_CODE_LEN;

        /*@brief �û�������Ϣ�ַ�������*/
        public const int IMOS_USER_CODE_LEN = IMOS_CODE_LEN;

        /*@brief �������Ϣ�ַ�������*/
        public const int IMOS_DOMAIN_CODE_LEN = IMOS_CODE_LEN;

        /*@brief ��������Ϣ�ַ������� */
        public const int IMOS_DOMAIN_NAME_LEN = IMOS_NAME_LEN;

        /*@brief Ȩ�ޱ�����Ϣ�ַ�������*/
        public const int IMOS_AUTH_CODE_LEN = IMOS_CODE_LEN;

        //ÿ�β�ѯʱ���ص������������Ľ���ĸ���
        public const int QUERY_ITEM_MAX = 200;

        /*@brief imos_time ʱ����Ϣ�ַ������� "2008-10-02 09:25:33.001 GMT" */
        public const int IMOS_TIME_LEN = 32;

        /*@brief �ļ������� */
        public const int IMOS_FILE_NAME_LEN = 64;

        public const uint ERR_XP_FAIL_TO_SETUP_PROTOCOL = 0x0007B0;      /**< ��������Э��ʧ�� */
        public const uint ERR_XP_FAIL_TO_PLAY_PROTOCOL = 0x0007B1;      /**< ����Э�̲���ʧ�� */
        public const uint ERR_XP_FAIL_TO_PAUSE_PROTOCOL = 0x0007B2;      /**< ����Э����ͣʧ�� */
        public const uint ERR_XP_FAIL_TO_STOP_PROTOCOL = 0x0007B3;      /**< ֹͣ����Э��ʧ�� */
        public const uint ERR_XP_RTSP_COMPLETE = 0x0007B4;      /**< RTSP���Ż�������� */
        public const uint ERR_XP_RTSP_ABNORMAL_TEATDOWN = 0x0007B5;      /**< RTSP�쳣���ߣ���������ȡ�ļ���������ݱ���д */
        public const uint ERR_XP_RTSP_KEEP_LIVE_TIME_OUT = 0x0007B6;      /**< RTSP����ʧ�� */
        public const uint ERR_XP_RTSP_ENCODE_CHANGE = 0x0007B7;      /**< RTSP��������ʽ�л� */
        public const uint ERR_XP_RTSP_DISCONNECT = 0x0007B8;      /**< RTSP���ӶϿ����㲥�طŻ��������Զ���ֹ���������� */

        public const uint ERR_XP_DISK_CAPACITY_WARN = 0x00079B;      /**< Ӳ��ʣ��ռ������ֵ */
        public const uint ERR_XP_DISK_CAPACITY_NOT_ENOUGH = 0x00079C;     /**< Ӳ��ʣ��ռ䲻�㣬�޷�����ҵ�� */
        public const uint ERR_XP_FAIL_TO_WRITE_FILE = 0x000723;     /**< д�ļ�����ʧ�� */
        public const uint ERR_XP_FAIL_TO_PROCESS_MEDIA_DATA = 0x0007A9;   /**< ý�����ݴ���ʧ�� */
        public const uint ERR_XP_NOT_SUPPORT_MEDIA_ENCODE_TYPE = 0x000735;/**< ����ͨ����ý������ʽ��֧�ִ˲��� */
        public const uint ERR_XP_MEDIA_RESOLUTION_CHANGE = 0x000736;      /**< ����ͨ����ý�����ֱ��ʷ����仯 */

        /*@brief imos_description ������Ϣ�ַ������� */
        public const int IMOS_DESC_LEN = (128 * 3);

        public const int IMOS_IE_URL_LEN = 512;

        public const int IMOS_PASSWD_ENCRYPT_LEN = 64;

        public const int IMOS_QUERY_ITEM_MAX_NUM = 16;

        public const int IMOS_QUERY_DATA_MAX_LEN = 64;

        public const int IMOS_DEV_STATUS_ONLINE = 1;

        public const int IMOS_DEV_STATUS_OFFLINE = 2;

        public const int IMOS_STREAM_MAXNUM = 2;

        public const int IMOS_MS_MAX_NUM_PER_CHANNEL = 1;

        public const int IMOS_DETECT_AREA_MAXNUM = 4;

        public const int IMOS_MASK_AREA_MAXNUM = 4;

        public const int IMOS_OSD_NAME_MAXNUM = 1;



        public static LOGIN_INFO_S stLoginInfo;
        public static XP_INFO_S stXpInfo;
        public static PLAY_WND_INFO_S[] astPlayWndInfo = new PLAY_WND_INFO_S[25];


        public static System.Timers.Timer timerKeepalive;

        public delegate void MethodInvoke1<T>(T Param);

        public static string UTF8ToUnicode(byte[] bufferIn)
        {

            byte[] buffer = Encoding.Convert(Encoding.UTF8, Encoding.Default, bufferIn, 0, bufferIn.Length);
            return Encoding.Default.GetString(buffer, 0, buffer.Length);
        }

        public static byte[] UnicodeToUTF8(string buffIn)
        {
            byte[] buffer = Encoding.Default.GetBytes(buffIn);
            return Encoding.Convert(Encoding.Default, Encoding.UTF8, buffer, 0, buffer.Length);
        }

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_Initiate(String strServerIP, UInt32 ulServerPort, UInt32 bUIFlag, UInt32 bXPFlag);

        /// <summary>
        /// xp��Ϣ�ص�����Ҫ���ڽ���һЩXP�����Ϣ
        /// </summary>
        /// <param name="stUserLoginIDInfo"></param>
        /// <param name="ulRunInfoType"></param>
        /// <param name="ptrInfo"></param>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate void XP_RUN_INFO_CALLBACK_EX_PF(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, UInt32 ulRunInfoType, IntPtr pParam);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_Encrypt(String strInput, UInt32 ulInLen, IntPtr ptrOutput);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_Login(String strUserLoginName, String strPassword, String strIpAddr, IntPtr ptrSDKLoginInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_LoginEx(String strUserLoginName, String strPassword, String srvIpAddr, String cltIpAddr, IntPtr ptrSDKLoginInfo);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_PlaySound(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_CleanUp(IntPtr pstUserLogIDInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_UserKeepAlive(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartMonitor(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCameraCode, byte[] szMonitorCode, UInt32 ulStreamType, UInt32 ulOperateCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopMonitor(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szMonitorCode, UInt32 ulOperateCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryResourceList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, UInt32 ulResType, UInt32 ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrResList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern UInt32 IMOS_QueryResourceListV2(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szOrgCode, ref COMMON_QUERY_CONDITION_S pstQueryCondition, ref QUERY_PAGE_INFO_S pstQueryPageInfo, ref RSP_PAGE_INFO_S pstRspPageInfo, IntPtr pstResList);

        [DllImport("imos_sdk.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_QueryResourceListV2(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, string orgCode, ref COMMON_QUERY_CONDITION_S stQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, ref RSP_PAGE_INFO_S stRspPage, IntPtr ptrResList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_Logout(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void IMOS_LogoutEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void IMOS_GetChannelCode(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, IntPtr pcChannelCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartPtzCtrl(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ReleasePtzCtrl(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, UInt32 bReleaseSelf);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryPresetList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, ref QUERY_PAGE_INFO_S stQueryPageInfo, ref RSP_PAGE_INFO_S ptrRspPage, IntPtr pstPresetList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPreset(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szCamCode, ref PRESET_INFO_S pstPreset);

        /// <summary>
        /// ɾ��Ԥ��λ
        /// </summary>
        /// <param name="pstUserLogIDInfo">�û���¼ID��Ϣ��ʶ</param>
        /// <param name="szCamCode">���������</param>
        /// <param name="ulPresetValue">Ԥ��λֵ</param>
        /// <returns></returns>
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_DelPreset(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szCamCode, UInt32 ulPresetValue);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_UsePreset(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szCamCode, UInt32 ulPresetNum);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_PtzCtrlCommand(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, ref PTZ_CTRL_COMMAND_S stPTZCommand);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartPlayer(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, UInt32 ulPlayWndNum, IntPtr ptrPlayWndInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopPlayer(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPlayWnd(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr hWnd);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_RecordRetrieval(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref REC_QUERY_INFO_S stSDKRecQueryInfo, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrSDKRecordFileInfo);

        /// <summary>
        /// ע��ص�����
        /// </summary>
        /// <param name="pstUserLoginIDInfo">������Ϣ</param>
        /// <param name="ptrCallBack">�ص�����</param>
        /// <returns></returns>
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_RegCallBackPrcFunc(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, IntPtr pfnCallBackProc);

        /// <summary>
        /// �澯�ص�����
        /// </summary>
        /// <param name="ulProcType">�澯����</param>
        /// <param name="ptrParam">���ص�����ָ��</param>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate void CALL_BACK_PROC_PF(UInt32 ulProcType, IntPtr ptrParam);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetRecordFileURL(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref GET_URL_INFO_S stSDKGetUrlInfo, ref URL_INFO_S stUrlInfo);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_FreeChannelCode(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, Byte[] pcChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_OpenVodStream(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szVodUrl, byte[] szServerIP, UInt16 usServerPort, UInt32 ulProtl);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartPlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_PausePlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ResumePlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopPlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPlaySpeed(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, UInt32 ulPlaySpeed);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetRunMsgCB(IntPtr ptrRunInfoFunc);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetDownloadTime(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, String pcDownloadID, byte[] pszTime);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_OneByOne(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPlayedTime(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szTime);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPlayedTimeEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, UInt32 ulTime);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetPlayedTime(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrTime);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetPlayedTimeEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrTime);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopDownload(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcDownloadID);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SnatchOnce(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulPicFormat);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SnatchOnceEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulPicFormat);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartSnatchSeries(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulPicFormat, UInt32 ulInterval);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopSnatchSeries(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartRecord(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulFileFormat);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartRecordEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulFileFormat, IntPtr ptrFilePostfix);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopRecord(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetVideoEncodeType(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, ref UInt32 ptrVideoEncodeType);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetLostPacketRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrRecvPktNum, IntPtr ptrLostPktNum);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ResetLostPacketRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetLostFrameRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrAllFrameNum, IntPtr ptrLostFrameNum);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ResetLostFrameRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_OpenDownload(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcDownUrl, byte[] pcServerIP, ushort usServerPort, UInt32 ulProtl, UInt32 ulDownMediaSpeed, byte[] pcFileName, UInt32 ulFileFormat, byte[] pcChannelCode);


        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartDownload(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode);



        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetDecoderTag(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode, byte[] pcDecorderTag);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetFrameRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, ref UInt32 ptrFrameRate);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetBitRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, ref UInt32 ptrBitRate);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopSound(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetVolume(UInt32 ulVolume);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetVolume(IntPtr ptrVolume);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetFieldMode(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, UInt32 ulFieldMode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_AdjustAllWaveAudio(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, UInt32 ulCoefficient);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_AdjustPktSeq(Boolean bAdjust);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetRenderMode(UInt32 ulRenderMode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetRealtimeFluency(UInt32 ulFluency);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_XP_SetDeinterlaceMode(UInt32 ulPort, UInt32 ulDeinterlaceMode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPixelFormat(UInt32 ulPixelFormat);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigXpStreamInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref XP_STREAM_INFO_S pstXpStreamInfo);



        //EC Camera ���ýӿ�

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_AddEc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref EC_INFO_S stEcInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ModifyEc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref EC_INFO_S stEcInfo, UInt32 IsEncodeChange);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_DelEc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szEcCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryEcList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, IntPtr ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrEcList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryEcInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szEcCode, IntPtr ptrEcInfo);
        [DllImport("imos_sdk.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_QueryEcInfo(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szEcCode, ref EC_INFO_S pstEcInfo);
        [DllImport("imos_sdk.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_QueryEcInfo(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, string ecCode, ref EC_INFO_S pstEcInfo);
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_BindCameraToVideoInChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref VINCHNL_BIND_CAMERA_S stVinChnlAndCamInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryCameraAndChannelList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDevCode, IntPtr stQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrCamAndChannelList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ModifyCamera(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref CAMERA_INFO_S stCamInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_UnBindCamera(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryCamera(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, IntPtr ptrCameraInfo);
        [DllImport("imos_sdk.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_QueryCamera(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, string cameraCode, ref CAMERA_INFO_S pstCameraInfo);
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryCameraEX(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, IntPtr ptrCameraInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigVideoInChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref VIN_CHANNEL_S stVideoInChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryECVideoInChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrVideoInChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigECVideoStream(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref VIDEO_STREAM_S stVideoStreamInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryECVideoStream(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrStreamNum, IntPtr ptrVideoStreamInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigECMaskAreaOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulMaskAreaNum, ref VIDEO_AREA_S stMaskArea);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryECMaskAreaOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrMaskAreaNum, IntPtr ptrMaskArea);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigECMotionDetectArea(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulMotionDetectAreaNum, ref VIDEO_AREA_S stMotionDetectArea);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryECMotionDetectArea(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrMotionDetectAreaNum, IntPtr ptrMotionDetectArea);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigDeviceTimeOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref OSD_TIME_S stTimeOSD);

        /// <summary>
        /// ��ɶ������͹���
        /// </summary>
        /// <param name="stUserLoginIDInfo">������Ϣ</param>
        /// <param name="ulSubscribePushType">��������</param>
        /// <returns></returns>
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SubscribePushInfo(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, UInt32 ulSubscribePushType);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDeviceTimeOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrTimeOSD);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigDeviceNameOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulNameOSDNum, ref OSD_NAME_S stNameOSD);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDeviceNameOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrNameOSDNum, IntPtr ptrNameOSD);



        //DC Screen ���ýӿ�

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_AddDc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DC_INFO_S stDcInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ModifyDc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DC_INFO_S stDcInfo, UInt32 IsEncodeChange);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_DelDc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDcCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDcList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, IntPtr ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrDcList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDcInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDcCode, IntPtr ptrDcInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_BindScreenToVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref VOUTCHNL_BIND_SCREEN_S stVOUTChnlAndScrInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryScreenAndChannelList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDevCode, IntPtr ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrVOUTChnlAndScrList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ModifyScreen(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref SCREEN_INFO_S stScrInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_UnBindScreen(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szScrCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryScreen(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szScrCode, IntPtr ptrScreenInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigDCVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulVideoOutNum, ref VOUT_CHANNEL_S stVideoOutChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDCVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrVideoOutNum, IntPtr ptrVideoOutChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigDCPhyOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref PHYOUT_CHANNEL_S stPhyoutChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDCVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrPhyoutChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QuerySplitScrInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szScrCode, ref SPLIT_SCR_INFO_S ptrSplitInfo);


        //A8��������
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_OpenWindow(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref TV_WALL_WINDOWS_INFO st_TV_Wall_Windows_Info);


        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_CloseWindow(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] pcWindowCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ModWindow(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref TV_WALL_WINDOWS_INFO st_TV_Wall_Windows_Info, bool bSingleDB);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetWindowLevel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref TV_WALL_WINDOWS_INFO pstTVWallInfo, UInt32 ulWindowNum);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopA8Monitor(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szMonitorCode, UInt32 ulOperateCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartA8Monitor(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCameraCode, byte[] szMonitorCode, UInt32 ulStreamType, UInt32 ulOperateCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryTVWallA8(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] pcTVWallCode, IntPtr pstTVWallInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryTVWallListA8(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref COMMON_QUERY_CONDITION_S pstQueryCondition, ref QUERY_PAGE_INFO_S pstQueryPageInfo, IntPtr pstRspPageInfo, IntPtr pstTVWallList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetTVWallTransparency(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] pcWindowCode, UInt32 ulTransparency);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_TVWallSceneEnable(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] pcTVWallCode, UInt32 ulSceneID);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryTVWallSceneListA8(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] pcTVWallCode, UInt32 pulSceneNum, IntPtr pstTVWallSceneList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryA8(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szA8Code, IntPtr pstA8Info);


        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryA8List(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, ref COMMON_QUERY_CONDITION_S pstQueryCondition, ref QUERY_PAGE_INFO_S pstQueryPageInfo, ref RSP_PAGE_INFO_S pstRspPageInfo, ref A8_INFO_S pstA8List);


        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetA8SmoothDisplay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDevCode, UInt32 ulIsSmoothDisplay);


        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetA8SmoothDisplay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDevCode, IntPtr pulIsSmoothDisplay);


        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetTVWallAutoAdsorbent(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szTVWallCode, UInt32 ulIsAutoAdsorbent);

        [DllImport("xp_frame.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_SetDigitalZoom(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, string strChannelCode, IntPtr hWnd, ref XP_RECT_S stRect);

        [DllImport("xp_frame.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_SetDigitalZoom(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, string strChannelCode, IntPtr hWnd, IntPtr stRect);

    }
}
