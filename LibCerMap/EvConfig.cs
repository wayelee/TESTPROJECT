using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCerMap
{
    class EvConfig
    {
        public static string CenterlineXField = "经度";
        public static string CenterlineYField = "纬度";
        public static string CenterlineZField = "高程";
        public static string CenterlineMeasureField = "里程";
        public static string CenterlinePointTypeField = "测点属性";
        
        public static string IMUMoveDistanceField = "记录距离";
        public static string IMUPointTypeField = "类型";
        public static string IMULengthField = "长度";
        public static string IMUWidththField = "宽度";
        public static string IMUDepthField = "壁厚";
        public static string IMUAngleField = "时钟方位";
        public static string IMUAlignmentMeasureField = "对齐里程";
        public static string IMUInspectionYearField = "检测年份";

        public static string WeldXField = "经度";
        public static string WeldYField = "纬度";
        public static string WeldZField = "高程";
        public static string WeldMField = "里程";
        public static string WeldAlignmentMeasureField = "对齐里程";
    }
}
