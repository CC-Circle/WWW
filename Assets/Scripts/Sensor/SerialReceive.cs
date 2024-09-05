using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialReceive : MonoBehaviour
{
    public SerialHandler serialHandler;

    private float accX, accY, accZ; // 加速度
    public float gyroX, gyroY, gyroZ; // 角速度(視点変更で使用)
    public float pitch, roll, yaw; // 磁気
    private float button_flag; // ボタンのフラグ

    public int Flag { get; set; }
    public int Flag_view { get; set; }
    private int Flag_temp_left = 0;
    private int Flag_temp_right = 0;

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;

    }

    // Ardino側のプログラムによって変更が必要
    void OnDataReceived(string message)
    {
        // Example of received message:
        // "Accel: 0.00, 0.00, 0.00\tGyro: 0.00, 0.00, 0.00\tMag: 0.00, 0.00, 0.00\tTemp: 0.00"

        try
        {
            // Split the message into sections based on tabs
            string[] sections = message.Split('\t');
            if (sections.Length >= 4)
            {
                // 加速度のデータを切り離し，それぞれの変数に代入
                string[] accelData = sections[0].Substring(7).Split(',');
                if (accelData.Length == 3)
                {
                    accX = float.Parse(accelData[0]);
                    accY = float.Parse(accelData[1]);
                    accZ = float.Parse(accelData[2]);
                }

                // ジャイロのデータを切り離し，それぞれの変数に代入
                string[] gyroData = sections[1].Substring(6).Split(',');
                if (gyroData.Length == 3)
                {
                    gyroX = float.Parse(gyroData[0]);
                    gyroY = float.Parse(gyroData[1]);
                    gyroZ = float.Parse(gyroData[2]);
                }

                // 磁気のデータを切り離し，それぞれの変数に代入
                string[] magData = sections[2].Substring(5).Split(',');
                if (magData.Length == 3)
                {
                    pitch = float.Parse(magData[0]);
                    roll = float.Parse(magData[1]);
                    yaw = float.Parse(magData[2]);
                }

                // 温度のデータを切り離し，それぞれの変数に代入
                string tempData = sections[3].Substring(6);
                button_flag = float.Parse(tempData);

                // 加速度のデータを表示
                // Debug.Log($"Received accel data accX: {accX}, Y:{accY}, Z:{accZ}");
                // ジャイロのデータを表示
                // Debug.Log($"Received accel data: X:{gyroX}, Y:{gyroY}, Z:{gyroZ}");
                //ボタンのデータを表示
                // Debug.Log($"Received button data: {button_flag}");

                // 移動のためのフラッグ
                if (accX > 0.8f)
                {
                    // 右
                    Flag = 1;
                }
                else if (accX < -0.8f)
                {
                    // 左
                    Flag = 2;
                }

                // 視点変更のflag
                if (accX > 0.2) {
                    Flag_temp_left++;
                    if (Flag_temp_left == 20) {
                        Flag_view = 1;
                        Flag_temp_left = 0;
                    }
                } else if (accX < -0.2) {
                    Flag_temp_right++;
                    if (Flag_temp_right == 20) {
                        Flag_view = 2;
                        Flag_temp_right = 0;
                    }
                }
                // Debug.Logへの表示
                // Debug.Log(Flag);
            }
            else
            {
                // Debug.LogWarning("Insufficient data sections after splitting.");
            }
        }
        catch (System.Exception e)
        {
            // Debug.LogError($"Error parsing data: {e.Message}");
        }
    }
}