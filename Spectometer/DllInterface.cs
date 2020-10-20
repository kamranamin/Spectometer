using System;
using System.Runtime.InteropServices;

namespace RA_Camera_Core
{
    public class DllInterface
    {

#if WIN32 
        const string RA_Camera_Core_Lib = "RA_Camera_Core32.dll";
#elif WIN64
        const string RA_Camera_Core_Lib = "RA_Camera_Core64.dll";
#endif


        //################## Camera #####################
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        
        public extern static int Camera_ListConnectedDevices();//returns number of connected devices
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int Camera_GetConnectedDeviceProperties(int index,
                                                                   IntPtr serialBuf/*64 bytes, to be filled*/,
                                                                   IntPtr descBuf/*64 bytes, to be filled*/
                                                                   ); //returns : 1 on success 
        //------------------------------------


        //################## Line Scan #####################
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_Add();//returns Line Scan Camera Index
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_Remove(int lineScanCameraIndex);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_StartCameraCapture(int lineScanCameraIndex, IntPtr serialBuf/*at most 64 bytes*/, uint inTransferSize = 64 * 1024/*64KB*/, byte latencyTimerValue = 72);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public unsafe extern static int LineScanCamera_StartCameraCapture_WithGl(int lineScanCameraIndex, IntPtr serialBuf/*at most 64 bytes*/,
                                                                       int addScanner/*1: yes, 0: no*/, void* scannerWinHandle,
                                                                       int addGraph/*1: yes, 0: no*/, void* graphWinHandle,
                                                                       uint inTransferSize = 64 * 1024/*64KB*/,
                                                                       byte latencyTimerValue = 72);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_StopCameraCapture(int lineScanCameraIndex); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GetExtraBytesCount(int lineScanCameraIndex);//(call after StartCameraCapture) 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GetImageWidth(int lineScanCameraIndex);//(call after StartCameraCapture)  
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GetImageHeight(int lineScanCameraIndex); //(call after StartCameraCapture)  
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GetImageBytesPerPixel(int lineScanCameraIndex); //(call after StartCameraCapture) 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GetImageColoringType(int lineScanCameraIndex);//8: monochrome,  1:RGB     (call after StartCameraCapture)  
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_With2Lines(int lineScanCameraIndex);//returns 1 for true     (call after StartCameraCapture) 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_StartSaving(int lineScanCameraIndex,
                                                             IntPtr path/*256 bytes*/,
                                                             IntPtr imgPrefix/*256 bytes*/,
                                                             int imgWidth,
                                                             IntPtr rawFileName/*256 bytes*/,
                                                             int savingType/*image: 0, raw : 1 , image + raw : 2*/,
                                                             uint maxSavingFrames = 100000000,
                                                             float maxSavingTimeInSec = 50000); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_StopSaving(int lineScanCameraIndex);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_IsRunning(int lineScanCameraIndex);//returns 1 for true 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_IsSaving(int lineScanCameraIndex);//returns 1 for true 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetRedExpousure(int lineScanCameraIndex, uint val);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetGreenExpousure(int lineScanCameraIndex, uint val);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetBlueExpousure(int lineScanCameraIndex, uint val);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetRedContrast(int lineScanCameraIndex, uint val);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetGreenContrast(int lineScanCameraIndex, uint val);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetBlueContrast(int lineScanCameraIndex, uint val);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetRedBrightness(int lineScanCameraIndex, uint val);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetGreenBrightness(int lineScanCameraIndex, uint val);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetBlueBrightness(int lineScanCameraIndex, uint val);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetTrigMode(int lineScanCameraIndex, uint val);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_AvailableImageFrames(int lineScanCameraIndex);
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public unsafe extern static int LineScanCamera_GetImageFrame_ByArray(int lineScanCameraIndex, byte* rawDataBuf/*pass NULL(0) if you don't need it*/, byte* extraBytesBuf/*pass NULL(0) if you don't need it*/, int alsoUpdateScanner/*1: yes, 0: no*/, int alsoUpdateGraph/*1: yes, 0: no*/);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_DiscardAllAvailableImageFramesTillNow(int lineScanCameraIndex);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_DiscardAvailableImageFrames(int lineScanCameraIndex, int uptoCount);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SendCommand(int lineScanCameraIndex, byte b1, byte b2, byte b3, byte b4, byte b5);//use when camera is running, //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GlGraph_Resize(int lineScanCameraIndex, int w, int h);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public unsafe extern static int LineScanCamera_GlGraph_HorVerValuesUnderCursor(int lineScanCameraIndex, int x, int y, float* horVal, float* verValue);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GlGraph_MousePress(int lineScanCameraIndex, int x, int y, int btn/*1:left  ,  2:right*/);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GlGraph_MouseRelease(int lineScanCameraIndex, int x, int y, int btn/*1:left  ,  2:right*/);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GlGraph_MouseMove(int lineScanCameraIndex, int x, int y, int btn/*1:left  ,  2:right*/);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GlGraph_Mousewheel(int lineScanCameraIndex, int x, int y, int delta);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GlGraph_Render(int lineScanCameraIndex);// returns render count on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GlScanner_Resize(int lineScanCameraIndex, int w, int h);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_GlScanner_Render(int lineScanCameraIndex);// returns render count on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SignalProcessing_Disable(int lineScanCameraIndex);// returns 1 on success  (call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SignalProcessing_BoxCar_Enable(int lineScanCameraIndex, uint n/*1<->1000000*/);// returns 1 on success  (call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SignalProcessing_Threshold_Enable(int lineScanCameraIndex, uint threshold/*1<->64*1024-2*/, uint downValue = 0/*0<->64*1024-2*/, uint upValue = 1/*1<->64*1024-1*/);// returns 1 on success  (call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SignalProcessing_ContinusAverage_Enable(int lineScanCameraIndex, uint n/*2<->1000000*/);// returns 1 on success  (call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SignalProcessing_BundleAverage_Enable(int lineScanCameraIndex, uint n/*2<->1000000*/);// returns 1 on success  (call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SignalProcessing_ShadingCorrection_Enable(int lineScanCameraIndex, uint refPointID/*zero-based*/);// returns 1 on success  (call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public unsafe extern static int LineScanCamera_SignalProcessing_ShadingCorrection_SetFactors_ByArray(int lineScanCameraIndex, float* factors, uint componentID/*zero-based*/);// returns 1 on success (call when ShadingCorrection is Enabled)  (call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public unsafe extern static int LineScanCamera_SignalProcessing_ShadingCorrection_GetFactors_ByArray(int lineScanCameraIndex, float* factors, uint componentID/*zero-based*/);// returns 1 on success (call when ShadingCorrection is Enabled)  (call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetCALIB1(int lineScanCameraIndex, byte val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetCALIB2(int lineScanCameraIndex, byte val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetCALIB3(int lineScanCameraIndex, byte val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetCALIB4(int lineScanCameraIndex, byte val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetTrigDelay(int lineScanCameraIndex, uint val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetExposureMode(int lineScanCameraIndex, byte state); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetLineCntReset(int lineScanCameraIndex);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetTrigCntReset(int lineScanCameraIndex);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetSpeedLevel(int lineScanCameraIndex, byte state);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetTrigFactor(int lineScanCameraIndex, uint val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetTrigPolarity(int lineScanCameraIndex, byte state);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetSoftwareTrig(int lineScanCameraIndex);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetLineFreq(int lineScanCameraIndex, uint val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetFrameFreq(int lineScanCameraIndex, uint val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetMaxExpousure(int lineScanCameraIndex, uint val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetDecouplingLineFreq(int lineScanCameraIndex, byte state); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int LineScanCamera_SetDefectPixelCorrection(int lineScanCameraIndex, byte state, uint val); //returns 1 on success 
        //------------------------------------
        
        //################## Area Scan #####################
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_Add();// returns Area Scan Camera Index
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_Remove(int areaScanCameraIndex);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_StartCameraCapture(int areaScanCameraIndex, IntPtr serialBuf/*at most 64 bytes*/,
                                                                    int imgFieldType /*0: full field, 1: half field*/,
                                                                    uint inTransferSize = 64 * 1024/*64KB*/,
                                                                    byte latencyTimerValue = 72);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public unsafe extern static int AreaScanCamera_StartCameraCapture_WithGl(int areaScanCameraIndex, IntPtr serialBuf/*at most 64 bytes*/, int imgFieldType /*0: full field, 1: half field*/,
                                                                       int addMonitor/*1: yes, 0: no*/, void* monitorWinHandle,
                                                                       uint inTransferSize = 64 * 1024/*64KB*/,
                                                                       byte latencyTimerValue = 72);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_StopCameraCapture(int areaScanCameraIndex);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_GetExtraBytesCount(int areaScanCameraIndex);//(call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_GetImageWidth(int areaScanCameraIndex);//(call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_GetImageHeight(int areaScanCameraIndex); //(call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_GetImageBytesPerPixel(int areaScanCameraIndex); //(call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_GetImageColoringType(int areaScanCameraIndex);//8: monochrome,  1:RGB     (call after StartCameraCapture)
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_StartSaving(int areaScanCameraIndex,
                                                             IntPtr path/*256 bytes*/,
                                                             IntPtr imgPrefix/*256 bytes*/,
                                                             IntPtr rawFileName/*256 bytes*/,
                                                             int savingType/*image: 0, raw : 1 , image + raw : 2*/,
                                                             uint maxSavingFrames = 100000000,
                                                             float maxSavingTimeInSec = 50000); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_StopSaving(int areaScanCameraIndex);//returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_IsRunning(int areaScanCameraIndex);//returns 1 for true 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_IsSaving(int areaScanCameraIndex);//returns 1 for true 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetExpousure(int areaScanCameraIndex, uint val);//returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetContrast(int areaScanCameraIndex, uint val);//returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetBrightness(int areaScanCameraIndex, uint val);//returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetTrigMode(int areaScanCameraIndex, uint val);//returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_AvailableImageFrames(int areaScanCameraIndex);
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public unsafe extern static int AreaScanCamera_GetImageFrame_ByArray(int areaScanCameraIndex, byte* rawDataBuf/*pass NULL(0) if you don't need it*/, byte* extraBytesBuf/*pass NULL(0) if you don't need it*/, int alsoUpdateMonitor/*1: yes, 0: no*/);
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_DiscardAllAvailableImageFramesTillNow(int areaScanCameraIndex);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_DiscardAvailableImageFrames(int areaScanCameraIndex, int uptoCount);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SendCommand(int areaScanCameraIndex, byte b1, byte b2, byte b3, byte b4, byte b5);//use when camera is running // returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_GlMonitor_Resize(int areaScanCameraIndex, int w, int h);// returns 1 on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_GlMonitor_Render(int areaScanCameraIndex);// returns render count on success
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetCALIB1(int areaScanCameraIndex, byte val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetCALIB2(int areaScanCameraIndex, byte val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetCALIB3(int areaScanCameraIndex, byte val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetCALIB4(int areaScanCameraIndex, byte val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetTrigDelay(int areaScanCameraIndex, uint val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetExposureMode(int areaScanCameraIndex, byte state); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetLineCntReset(int areaScanCameraIndex);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetTrigCntReset(int areaScanCameraIndex);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetSpeedLevel(int areaScanCameraIndex, byte state);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetTrigFactor(int areaScanCameraIndex, uint val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetTrigPolarity(int areaScanCameraIndex, byte state);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetSoftwareTrig(int areaScanCameraIndex);//returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetLineFreq(int areaScanCameraIndex, uint val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetFrameFreq(int areaScanCameraIndex, uint val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetMaxExpousure(int areaScanCameraIndex, uint val); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetDecouplingLineFreq(int areaScanCameraIndex, byte state); //returns 1 on success 
        //------------------------------------
        [DllImport(RA_Camera_Core_Lib, CallingConvention = CallingConvention.Cdecl)]
        public extern static int AreaScanCamera_SetDefectPixelCorrection(int areaScanCameraIndex, byte state, uint val); //returns 1 on success 
        //------------------------------------
    }
}