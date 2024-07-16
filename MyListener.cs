using UnityEngine;
using System.Net.Sockets;
using System;
using System.Threading;

public class HandDataReceiver : MonoBehaviour
{
    Thread clientThread;
    TcpClient client;
    public string serverIP = "127.0.0.1";
    public int serverPort = 65535;
    bool isConnected = false;

    // Use these variables to store received hand data
    private Vector3 indexTip, indexDip, indexPip, indexMcp,
                    middleTip, middleDip, middlePip, middleMcp,
                    ringTip, ringDip, ringPip, ringMcp,
                    pinkyTip, pinkyDip, pinkyPip, pinkyMcp,
                    thumbTip, thumbDip, thumbMcp, thumbCmc, wrist, indexTipL, indexDipL, indexPipL, indexMcpL,
                    middleTipL, middleDipL, middlePipL, middleMcpL,
                    ringTipL, ringDipL, ringPipL, ringMcpL,
                    pinkyTipL, pinkyDipL, pinkyPipL, pinkyMcpL,
                    thumbTipL, thumbDipL, thumbMcpL, thumbCmcL, wristL;
    private Vector3 wristAdd;

    public Transform IndexTip_v;
    public Transform IndexDip_v;
    public Transform IndexPip_v;
    public Transform IndexMCP_v;

    public Transform MiddleTip_v;
    public Transform MiddleDip_v;
    public Transform MiddlePip_v;
    public Transform MiddleMCP_v;

    public Transform RingTip_v;
    public Transform RingDip_v;
    public Transform RingPip_v;
    public Transform RingMCP_v;

    public Transform PinkyTip_v;
    public Transform PinkyDip_v;
    public Transform PinkyPip_v;
    public Transform PinkyMCP_v;

    public Transform ThumbTip_v;
    public Transform ThumbDip_v;
    public Transform ThumbMCP_v;
    public Transform ThumbCMC_v;

    public Transform wristBone_v;


    public Transform IndexTip_vL;
    public Transform IndexDip_vL;
    public Transform IndexPip_vL;
    public Transform IndexMCP_vL;

    public Transform MiddleTip_vL;
    public Transform MiddleDip_vL;
    public Transform MiddlePip_vL;
    public Transform MiddleMCP_vL;

    public Transform RingTip_vL;
    public Transform RingDip_vL;
    public Transform RingPip_vL;
    public Transform RingMCP_vL;

    public Transform PinkyTip_vL;
    public Transform PinkyDip_vL;
    public Transform PinkyPip_vL;
    public Transform PinkyMCP_vL;

    public Transform ThumbTip_vL;
    public Transform ThumbDip_vL;
    public Transform ThumbMCP_vL;
    public Transform ThumbCMC_vL;

    public Transform wristBone_vL;

    void Start()
    {
        wristBone_v = transform.Find("Armature/Bone");

        IndexTip_v = transform.Find("Armature/Bone/Bone.001/Bone.004/Bone.005/Bone.006/Bone.006_end");
        IndexDip_v = transform.Find("Armature/Bone/Bone.001/Bone.004/Bone.005/Bone.006");
        IndexPip_v = transform.Find("Armature/Bone/Bone.001/Bone.004/Bone.005");
        IndexMCP_v = transform.Find("Armature/Bone/Bone.001/Bone.004");

        ThumbTip_v = transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003/Bone.003_end");
        ThumbDip_v = transform.Find("Armature/Bone/Bone.001/Bone.002/Bone.003");
        ThumbMCP_v = transform.Find("Armature/Bone/Bone.001/Bone.002");
        ThumbCMC_v = transform.Find("Armature/Bone/Bone.001");

        MiddleTip_v = transform.Find("Armature/Bone/Bone.001/Bone.007/Bone.008/Bone.009/Bone.009_end");
        MiddleDip_v = transform.Find("Armature/Bone/Bone.001/Bone.007/Bone.008/Bone.009");
        MiddlePip_v = transform.Find("Armature/Bone/Bone.001/Bone.007/Bone.008");
        MiddleMCP_v = transform.Find("Armature/Bone/Bone.001/Bone.007");

        RingTip_v = transform.Find("Armature/Bone/Bone.001/Bone.010/Bone.011/Bone.012/Bone.012_end");
        RingDip_v = transform.Find("Armature/Bone/Bone.001/Bone.010/Bone.011/Bone.012");
        RingPip_v = transform.Find("Armature/Bone/Bone.001/Bone.010/Bone.011");
        RingMCP_v = transform.Find("Armature/Bone/Bone.001/Bone.010");

        PinkyTip_v = transform.Find("Armature/Bone/Bone.001/Bone.013/Bone.014/Bone.015/Bone.015_end");
        PinkyDip_v = transform.Find("Armature/Bone/Bone.001/Bone.013/Bone.014/Bone.015");
        PinkyPip_v = transform.Find("Armature/Bone/Bone.001/Bone.013/Bone.014");
        PinkyMCP_v = transform.Find("Armature/Bone/Bone.001/Bone.013");


        wristBone_vL = transform.Find("Cube");

        IndexTip_vL = transform.Find("WhiteHand/HandRig/Wrist/IndexFinger/Index2/Index3/Bone.006_end");
        IndexDip_vL = transform.Find("WhiteHand/HandRig/Wrist/IndexFinger/Index2/Index3");
        IndexPip_vL = transform.Find("WhiteHand/HandRig/Wrist/IndexFinger/Index2");
        IndexMCP_vL = transform.Find("WhiteHand/HandRig/Wrist/IndexFinger");

        ThumbTip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/Thumb/Thumb2/Bone.003_end");
        ThumbDip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/Thumb/Thumb2");
        ThumbMCP_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/Thumb");
        ThumbCMC_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand");

        MiddleTip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/MiddleFinger/Middle2/Middle3/Bone.009_end");
        MiddleDip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/MiddleFinger/Middle2/Middle3");
        MiddlePip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/MiddleFinger/Middle2");
        MiddleMCP_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/MiddleFinger");

        RingTip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/RingFinger/Ring2/Ring3/Bone.012_end");
        RingDip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/RingFinger/Ring2/Ring3");
        RingPip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/RingFinger/Ring2");
        RingMCP_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/RingFinger");

        PinkyTip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/LittleFinger/Little2/Little3/Bone.015_end");
        PinkyDip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/LittleFinger/Little2/Little3");
        PinkyPip_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/LittleFinger/Little2");
        PinkyMCP_vL = transform.Find("WhiteHand/HandRig/Wrist/Hand/LittleFinger");

        wristAdd = new Vector3(0, -0.1f, 0);




        ConnectToServer();
    }

    void ConnectToServer()
    {
        clientThread = new Thread(() =>
        {
            client = new TcpClient(serverIP, serverPort);
            Debug.Log("Connected to server");
            isConnected = true;
            ReceiveData();
        });
        clientThread.Start();
    }

    void ReceiveData()
    {
        NetworkStream stream = client.GetStream();
        byte[] receivedBytes = new byte[504]; // Adjusted for both hands

        while (isConnected)
        {
            try
            {
                int bytesRead = stream.Read(receivedBytes, 0, receivedBytes.Length);
                if (bytesRead > 0)
                {
                    // Process received data for both hands

                    ProcessReceivedData(receivedBytes);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error receiving data: {e.Message}");
                isConnected = false;
            }
        }
    }

    void ProcessReceivedData(byte[] data)
    {
        // Define scaling factors for each axis
        float scaleX = 2.0f; // Example scaling factor for X
        float scaleY = -2.0f; // Example scaling factor for Y, negated if Y is inverted
        float scaleZ = -2.0f; // Example scaling factor for Z
       
            wristL = new Vector3(BitConverter.ToSingle(data, 0) * scaleX, BitConverter.ToSingle(data, 4) * scaleY, BitConverter.ToSingle(data, 8) * scaleZ);

            thumbCmcL = new Vector3(BitConverter.ToSingle(data, 12) * scaleX, BitConverter.ToSingle(data, 16) * scaleY, BitConverter.ToSingle(data, 20) * scaleZ);
            thumbMcpL = new Vector3(BitConverter.ToSingle(data, 24) * scaleX, BitConverter.ToSingle(data, 28) * scaleY, BitConverter.ToSingle(data, 32) * scaleZ);
            thumbDipL = new Vector3(BitConverter.ToSingle(data, 36) * scaleX, BitConverter.ToSingle(data, 40) * scaleY, BitConverter.ToSingle(data, 44) * scaleZ);
            thumbTipL = new Vector3(BitConverter.ToSingle(data, 48) * scaleX, BitConverter.ToSingle(data, 52) * scaleY, BitConverter.ToSingle(data, 56) * scaleZ);


            indexMcpL = new Vector3(BitConverter.ToSingle(data, 60) * scaleX, BitConverter.ToSingle(data, 64) * scaleY, BitConverter.ToSingle(data, 68) * scaleZ);
            indexPipL = new Vector3(BitConverter.ToSingle(data, 72) * scaleX, BitConverter.ToSingle(data, 76) * scaleY, BitConverter.ToSingle(data, 80) * scaleZ);
            indexDipL = new Vector3(BitConverter.ToSingle(data, 84) * scaleX, BitConverter.ToSingle(data, 88) * scaleY, BitConverter.ToSingle(data, 92) * scaleZ);
            indexTipL = new Vector3(BitConverter.ToSingle(data, 96) * scaleX, BitConverter.ToSingle(data, 100) * scaleY, BitConverter.ToSingle(data, 104) * scaleZ);

            middleMcpL = new Vector3(BitConverter.ToSingle(data, 108) * scaleX, BitConverter.ToSingle(data, 112) * scaleY, BitConverter.ToSingle(data, 116) * scaleZ);
            middlePipL = new Vector3(BitConverter.ToSingle(data, 120) * scaleX, BitConverter.ToSingle(data, 124) * scaleY, BitConverter.ToSingle(data, 128) * scaleZ);
            middleDipL = new Vector3(BitConverter.ToSingle(data, 132) * scaleX, BitConverter.ToSingle(data, 136) * scaleY, BitConverter.ToSingle(data, 140) * scaleZ);
            middleTipL = new Vector3(BitConverter.ToSingle(data, 144) * scaleX, BitConverter.ToSingle(data, 148) * scaleY, BitConverter.ToSingle(data, 152) * scaleZ);

            ringMcpL = new Vector3(BitConverter.ToSingle(data, 156) * scaleX, BitConverter.ToSingle(data, 160) * scaleY, BitConverter.ToSingle(data, 164) * scaleZ);
            ringPipL = new Vector3(BitConverter.ToSingle(data, 168) * scaleX, BitConverter.ToSingle(data, 172) * scaleY, BitConverter.ToSingle(data, 176) * scaleZ);
            ringDipL = new Vector3(BitConverter.ToSingle(data, 180) * scaleX, BitConverter.ToSingle(data, 184) * scaleY, BitConverter.ToSingle(data, 188) * scaleZ);
            ringTipL = new Vector3(BitConverter.ToSingle(data, 192) * scaleX, BitConverter.ToSingle(data, 196) * scaleY, BitConverter.ToSingle(data, 200) * scaleZ);

            pinkyMcpL = new Vector3(BitConverter.ToSingle(data, 204) * scaleX, BitConverter.ToSingle(data, 208) * scaleY, BitConverter.ToSingle(data, 212) * scaleZ);
            pinkyPipL = new Vector3(BitConverter.ToSingle(data, 216) * scaleX, BitConverter.ToSingle(data, 220) * scaleY, BitConverter.ToSingle(data, 224) * scaleZ);
            pinkyDipL = new Vector3(BitConverter.ToSingle(data, 228) * scaleX, BitConverter.ToSingle(data, 232) * scaleY, BitConverter.ToSingle(data, 236) * scaleZ);
            pinkyTipL = new Vector3(BitConverter.ToSingle(data, 240) * scaleX, BitConverter.ToSingle(data, 244) * scaleY, BitConverter.ToSingle(data, 248) * scaleZ);

        
            wrist = new Vector3(BitConverter.ToSingle(data, 252) * scaleX, BitConverter.ToSingle(data, 256) * scaleY, BitConverter.ToSingle(data, 260) * scaleZ);

            thumbCmc = new Vector3(BitConverter.ToSingle(data, 264) * scaleX, BitConverter.ToSingle(data, 268) * scaleY, BitConverter.ToSingle(data, 272) * scaleZ);
            thumbMcp = new Vector3(BitConverter.ToSingle(data, 276) * scaleX, BitConverter.ToSingle(data, 280) * scaleY, BitConverter.ToSingle(data, 284) * scaleZ);
            thumbDip = new Vector3(BitConverter.ToSingle(data, 288) * scaleX, BitConverter.ToSingle(data, 292) * scaleY, BitConverter.ToSingle(data, 296) * scaleZ);
            thumbTip = new Vector3(BitConverter.ToSingle(data, 300) * scaleX, BitConverter.ToSingle(data, 304) * scaleY, BitConverter.ToSingle(data, 308) * scaleZ);


            indexMcp = new Vector3(BitConverter.ToSingle(data, 312) * scaleX, BitConverter.ToSingle(data, 316) * scaleY, BitConverter.ToSingle(data, 320) * scaleZ);
            indexPip = new Vector3(BitConverter.ToSingle(data, 324) * scaleX, BitConverter.ToSingle(data, 328) * scaleY, BitConverter.ToSingle(data, 332) * scaleZ);
            indexDip = new Vector3(BitConverter.ToSingle(data, 336) * scaleX, BitConverter.ToSingle(data, 340) * scaleY, BitConverter.ToSingle(data, 344) * scaleZ);
            indexTip = new Vector3(BitConverter.ToSingle(data, 348) * scaleX, BitConverter.ToSingle(data, 352) * scaleY, BitConverter.ToSingle(data, 356) * scaleZ);

            middleMcp = new Vector3(BitConverter.ToSingle(data, 360) * scaleX, BitConverter.ToSingle(data, 364) * scaleY, BitConverter.ToSingle(data, 368) * scaleZ);
            middlePip = new Vector3(BitConverter.ToSingle(data, 372) * scaleX, BitConverter.ToSingle(data, 376) * scaleY, BitConverter.ToSingle(data, 380) * scaleZ);
            middleDip = new Vector3(BitConverter.ToSingle(data, 384) * scaleX, BitConverter.ToSingle(data, 388) * scaleY, BitConverter.ToSingle(data, 392) * scaleZ);
            middleTip = new Vector3(BitConverter.ToSingle(data, 396) * scaleX, BitConverter.ToSingle(data, 400) * scaleY, BitConverter.ToSingle(data, 404) * scaleZ);

            ringMcp = new Vector3(BitConverter.ToSingle(data, 408) * scaleX, BitConverter.ToSingle(data, 412) * scaleY, BitConverter.ToSingle(data, 416) * scaleZ);
            ringPip = new Vector3(BitConverter.ToSingle(data, 420) * scaleX, BitConverter.ToSingle(data, 424) * scaleY, BitConverter.ToSingle(data, 428) * scaleZ);
            ringDip = new Vector3(BitConverter.ToSingle(data, 432) * scaleX, BitConverter.ToSingle(data, 436) * scaleY, BitConverter.ToSingle(data, 440) * scaleZ);
            ringTip = new Vector3(BitConverter.ToSingle(data, 444) * scaleX, BitConverter.ToSingle(data, 448) * scaleY, BitConverter.ToSingle(data, 452) * scaleZ);

            pinkyMcp = new Vector3(BitConverter.ToSingle(data, 456) * scaleX, BitConverter.ToSingle(data, 460) * scaleY, BitConverter.ToSingle(data, 464) * scaleZ);
            pinkyPip = new Vector3(BitConverter.ToSingle(data, 468) * scaleX, BitConverter.ToSingle(data, 472) * scaleY, BitConverter.ToSingle(data, 476) * scaleZ);
            pinkyDip = new Vector3(BitConverter.ToSingle(data, 480) * scaleX, BitConverter.ToSingle(data, 484) * scaleY, BitConverter.ToSingle(data, 488) * scaleZ);
            pinkyTip = new Vector3(BitConverter.ToSingle(data, 492) * scaleX, BitConverter.ToSingle(data, 496) * scaleY, BitConverter.ToSingle(data, 500) * scaleZ);




    }
    void Update()
    {
        UpdateRightHandPositions();
        UpdateLeftHandPositions();
    }

    private void UpdateRightHandPositions()
    {
        if (wristBone_v != null)
        {
            wristBone_v.position = wrist;
        }
        else
        {
           Debug.LogWarning("wristBone_v Transform is not assigned.");
        }

        UpdateTransformPosition(ThumbTip_v, thumbTip, "ThumbTip_v");
        UpdateTransformPosition(ThumbDip_v, thumbDip, "ThumbDip_v");
        UpdateTransformPosition(ThumbMCP_v, thumbMcp, "ThumbMCP_v");
        UpdateTransformPosition(ThumbCMC_v, thumbCmc, "ThumbCMC_v");

        UpdateTransformPosition(IndexTip_v, indexTip, "IndexTip_v");
        UpdateTransformPosition(IndexDip_v, indexDip, "IndexDip_v");
        UpdateTransformPosition(IndexPip_v, indexPip, "IndexPip_v");
        UpdateTransformPosition(IndexMCP_v, indexMcp, "IndexMCP_v");

        UpdateTransformPosition(MiddleTip_v, middleTip, "MiddleTip_v");
        UpdateTransformPosition(MiddleDip_v, middleDip, "MiddleDip_v");
        UpdateTransformPosition(MiddlePip_v, middlePip, "MiddlePip_v");
        UpdateTransformPosition(MiddleMCP_v, middleMcp, "MiddleMCP_v");

        UpdateTransformPosition(RingTip_v, ringTip, "RingTip_v");
        UpdateTransformPosition(RingDip_v, ringDip, "RingDip_v");
        UpdateTransformPosition(RingPip_v, ringPip, "RingPip_v");
        UpdateTransformPosition(RingMCP_v, ringMcp, "RingMCP_v");

        UpdateTransformPosition(PinkyTip_v, pinkyTip, "PinkyTip_v");
        UpdateTransformPosition(PinkyDip_v, pinkyDip, "PinkyDip_v");
        UpdateTransformPosition(PinkyPip_v, pinkyPip, "PinkyPip_v");
        UpdateTransformPosition(PinkyMCP_v, pinkyMcp, "PinkyMCP_v");
    }

    private void UpdateLeftHandPositions()
    {


        if (wristBone_vL != null)
        {
            wristBone_vL.position = wristL;
        }
        else
        {
            Debug.LogWarning("wristBone_vL Transform is not assigned.");
        }

        UpdateTransformPosition(ThumbTip_vL, thumbTipL, "ThumbTip_vL");
        UpdateTransformPosition(ThumbDip_vL, thumbDipL, "ThumbDip_vL");
        UpdateTransformPosition(ThumbMCP_vL, thumbMcpL, "ThumbMCP_vL");
        UpdateTransformPosition(ThumbCMC_vL, thumbCmcL, "ThumbCMC_vL");

        UpdateTransformPosition(IndexTip_vL, indexTipL, "IndexTip_vL");
        UpdateTransformPosition(IndexDip_vL, indexDipL, "IndexDip_vL");
        UpdateTransformPosition(IndexPip_vL, indexPipL, "IndexPip_vL");
        UpdateTransformPosition(IndexMCP_vL, indexMcpL, "IndexMCP_vL");

        UpdateTransformPosition(MiddleTip_vL, middleTipL, "MiddleTip_vL");
        UpdateTransformPosition(MiddleDip_vL, middleDipL, "MiddleDip_vL");
        UpdateTransformPosition(MiddlePip_vL, middlePipL, "MiddlePip_vL");
        UpdateTransformPosition(MiddleMCP_vL, middleMcpL, "MiddleMCP_vL");

        UpdateTransformPosition(RingTip_vL, ringTipL, "RingTip_vL");
        UpdateTransformPosition(RingDip_vL, ringDipL, "RingDip_vL");
        UpdateTransformPosition(RingPip_vL, ringPipL, "RingPip_vL");
        UpdateTransformPosition(RingMCP_vL, ringMcpL, "RingMCP_vL");

        UpdateTransformPosition(PinkyTip_vL, pinkyTipL, "PinkyTip_vL");
        UpdateTransformPosition(PinkyDip_vL, pinkyDipL, "PinkyDip_vL");
        UpdateTransformPosition(PinkyPip_vL, pinkyPipL, "PinkyPip_vL");
        UpdateTransformPosition(PinkyMCP_vL, pinkyMcpL, "PinkyMCP_vL");
    }

    private void UpdateTransformPosition(Transform transform, Vector3 position, string transformName)
    {
        if (transform != null)
        {
            transform.position = position;
        }
        else
        {
          Debug.LogWarning($"{transformName} Transform is not assigned.");
        }
    }



    private void OnApplicationQuit()
    {
        isConnected = false;
        client?.Close();
        clientThread?.Abort();
    }
}