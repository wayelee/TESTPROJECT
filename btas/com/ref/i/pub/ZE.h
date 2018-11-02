
/**
\file       ZE.h
\brief      错误状态服务
            
            定义数据传输软件的各类错误码；
            提供通过错误码获取错误说明的编程接口。
            
\author     刘西昌
\date       2009-11-18
\version    1.0
\note       WINDOSW/C
\par        修改记录 
            - 刘西昌 2009-11-18 模块建立
            
*/


#ifndef _ZE_H__
#define _ZE_H__


/************************** 错误码 **********************************/
#define ZE_NOINIT           -1          /* 服务未初始化             */
#define ZE_INITSYN          -2          /* 初始化同步错误           */
#define ZE_MAPGSEC          -3          /* 映射全局段错误           */
#define ZE_INITADDR         -4          /* 初始化地址错误           */
#define ZE_INITDEV          -5          /* 初始化设备错误           */
#define ZE_INITQUE          -6          /* 初始化队列错误           */
#define ZE_SYN              -7          /* 同步操作错误             */
#define ZE_QUEID            -8          /* 队列ID错误               */
#define ZE_QUENUM           -9          /* 队列个数错误             */
#define ZE_QUEBUFLEN        -10         /* 队列缓存长度错误         */
#define ZE_QUEFULL          -11         /* 队列满                   */
#define ZE_SUBTYPE          -12         /* 订阅类型错误             */
#define ZE_SUBNUM           -13         /* 订阅个数错误             */
#define ZE_SUBINFO          -14         /* 订阅信息错误             */
#define ZE_PROTYPE          -15         /* 协议类型错误             */
#define ZE_MSGTYPE          -16         /* 消息类型错误             */
#define ZE_TASKCODE         -17         /* 任务码错误               */
#define ZE_OBJCODE          -18         /* 目标码错误               */
#define ZE_CRTQUEDEV        -19         /* 创建队列设备错误         */
#define ZE_SNDREQ           -20         /* 发送控制命令请求错误     */
#define ZE_RCVRLY           -21         /* 接收控制命令应答错误     */
#define ZE_SNDBUF           -22         /* 发送缓存错误             */
#define ZE_SNDBUFLEN        -23         /* 发送缓存长度错误         */
#define ZE_HIGHSNDFRE       -24         /* 发送频率超限             */
#define ZE_FORBIDSND        -25         /* 禁止发送                 */
#define ZE_WRITEDEV         -26         /* 设备写入数据错误         */
#define ZE_RCVBUF           -27         /* 接收缓存错误             */
#define ZE_RCVBUFLEN        -28         /* 接收缓存长度错误         */
#define ZE_READDEV          -29         /* 设备读取数据错误         */
#define ZE_NODATA           -30         /* 设备无数据               */
#define ZE_AIOFUNC          -31         /* 异步I/O入口错误          */
#define ZE_SETAIO           -32         /* 设置异步I/O错误          */
#define ZE_QUECODE          -33         /* 队列码错误               */
#define ZE_HOSTNAME         -34         /* 主机名错误               */
#define ZE_PROCNAME         -35         /* 进程名错误               */
#define ZE_LOCPATHTBLFULL   -36         /* 本地路径表满             */
#define ZE_LOCFREETBLFULL   -37         /* 本地空闲表满             */
#define ZE_REMPATHTBLFULL   -38         /* 网络路径表满             */
#define ZE_REMFREETBLFULL   -39         /* 网络空闲表满             */
#define ZE_INITNET          -40         /* 初始化网络服务错误       */
#define ZE_PARA             -41         /* 参数错误                 */
#define ZE_GETUSER          -42         /* 获取FTP账户错误          */
#define ZE_LOGIN            -43         /* 登录主机错误             */
#define ZE_SETTYPE          -44         /* 设置传输类型错误         */
#define ZE_SETPASV          -45         /* 设置服务器为被动模式错误 */
#define ZE_DATACON          -46         /* 建立数据连接错误         */
#define ZE_SETRETR          -47         /* 设置下载文件错误         */
#define ZE_SETSTOR          -48         /* 设置上传文件错误         */
#define ZE_OPENFILE         -49         /* 打开文件错误             */
#define ZE_READFILE         -50         /* 读文件错误               */
#define ZE_WRITEFILE        -51         /* 写文件错误               */
#define ZE_READSOCK         -52         /* 读SOCK错误               */
#define ZE_WRITESOCK        -53         /* 写SOCK错误               */
#define ZE_INITZM           -54         /* 初始化消息服务错误       */
#define ZE_GETHOSTNAME      -55         /* 获取主机名错误           */
#define ZE_GETPROCNAME      -56         /* 获取进程名错误           */
#define ZE_INDEX            -57         /* 获取任务全局段索引错误   */
#define ZE_ACCESS           -58         /* 越权操作                 */
#define ZE_UNLOAD           -59         /* 任务未加载               */
#define ZE_PATH             -60         /* 构造路径错误             */
#define ZE_RDCONF           -61         /* 读取配置文件错误         */
#define ZE_RATE             -62         /* 接收概率错误             */
#define ZE_CMDLEN           -63         /* 命令长度错误             */
#define ZE_FILETYPE         -64         /* 文件类型错误             */
#define ZE_FILENAME         -65         /* 文件名错误               */
#define ZE_DUPSTAT          -66         /* 处理机主备状态错误       */
#define ZE_TRANSTYPE        -67         /* 传输类型错误             */
#define ZE_FILENOEXIST      -68         /* 文件不存在               */
#define ZE_GETROOTPATH      -69         /* 获取服务根路径错误       */
#define ZE_PATHNAME         -70         /* 路径名错误               */
#define ZE_GETSYSFILENAME   -71         /* 获取系统文件名错误       */
#define ZE_COPYFILE         -72         /* 拷贝文件错误             */
#define ZE_FILEHEAD         -73         /* 文件头错误               */
#define ZE_CONSVR           -74         /* 连接服务器错误           */
#define ZE_FILENUM          -75         /* 文件个数错误             */
#define ZE_FILELEN          -76         /* 文件长度错误             */
#define ZE_TARFILE          -77         /* 打包文件错误             */
#define ZE_UNTARFILE        -78         /* 解包文件错误             */
#define ZE_ZIPFILE          -79         /* 压缩文件错误             */
#define ZE_UNZIPFILE        -80         /* 解压文件错误             */
#define ZE_HANDLER          -81         /* 句柄错误                 */
#define ZE_QRYCOND          -82         /* 查询条件错误             */
#define ZE_QRYRSLT          -83         /* 查询结果错误             */
#define ZE_POSITION         -84         /* 无效游标                 */
#define ZE_NULL             -85         /* 空值                     */
/********************************************************************/


/************************** 函数声明 ********************************/
#ifdef __cplusplus
extern "C" {
#endif

void ze_perror(int        siErrCode,
               const char szErrPre[]);  /* 获取错误说明             */
char* ze_strerr(int siErrCode);         /* 获取错误说明             */

#ifdef __cplusplus
}
#endif
/********************************************************************/


#endif
