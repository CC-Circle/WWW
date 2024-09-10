#define M5STACK_MPU6886
#define CALIBCOUNT 5000

#include <M5Stack.h>

float accX = 0.0F;
float accY = 0.0F;
float accZ = 0.0F;

float gyroX = 0.0F;
float gyroY = 0.0F;
float gyroZ = 0.0F;

float yaw = 0.0F;

float gyroOffsetZ = 0.0;

float preTime = 0.0F;
float dt = 0.0F;

float pregz = 0.0F;
float degree = 0;

int cnt = 0;

// 方向のフラッグ
int right_flag = 0;
int left_flag = 0;
int center_flag = 1;  // 0:HIGH 1:LOW

// ボタンのフラッグ
int Button_flag = 0;
int last_value_red = 0;
int cur_value_red = 0;
int last_value_blue = 0;
int cur_value_blue = 0;

void calibration() {
  delay(1000);  // キャリブレーション時間を1秒に設定
  M5.Lcd.printf("...");
  float gyroSumZ = 0;
  int count = CALIBCOUNT;
  for (int i = 0; i < count; i++) {
    M5.update();

    float gyroZ;
    M5.IMU.getGyroData(&gyroX, &gyroY, &gyroZ);

    gyroSumZ += gyroZ;
    if (M5.BtnB.wasPressed()) {
      M5.Lcd.clear();
      M5.Lcd.setCursor(140, 120);
      M5.Lcd.printf("Exit");
      delay(500);
      return;
    }
  }
  gyroOffsetZ = gyroSumZ / count - 0.02;
  M5.Lcd.clear();
  M5.Lcd.setCursor(140, 120);
  M5.Lcd.printf("Done");
  delay(500);
}

void GetGyro() {
  M5.IMU.getGyroData(&gyroX, &gyroY, &gyroZ);
  M5.IMU.getAccelData(&accX, &accY, &accZ);

  gyroZ -= gyroOffsetZ;

  dt = (micros() - preTime) / 1000000;
  preTime = micros();

  yaw -= (pregz + gyroZ) * dt / 2;
  pregz = gyroZ;

  if (yaw > 180) {
    yaw -= 360;
  } else if (yaw < -180) {
    yaw += 360;
  }
  delay(10);
}

void Button() {
  M5.update();
  if (M5.BtnA.wasPressed()) {
    cnt--;
    M5.Lcd.clear();
  }

  if (M5.BtnC.wasPressed()) {
    cnt++;
    M5.Lcd.clear();
  }
}

void ResetGyro() {
  gyroZ = 0.0;
  pregz = 0.0;
  yaw = 0.0;
  M5.Lcd.clear();
  M5.Lcd.setCursor(120, 120);
  M5.Lcd.printf("RESET");
  delay(500);
  M5.Lcd.clear();
}

bool getIsButton() {
  bool isButton = false;

  if (cur_value_blue != last_value_blue || cur_value_red != last_value_red) {
    if (cur_value_blue == 0 || cur_value_red == 0) {
      isButton = true;
    }
  }

  return isButton;
}

void Main() {
  M5.Lcd.clear();

  // フラッグの初期化
  right_flag = 0;
  left_flag = 0;
  center_flag = 1;

  while (true) {
    M5.update();

    // ボタンの状態を取得
    cur_value_red = digitalRead(26);
    cur_value_blue = digitalRead(36);

    // ボタンの操作内容の表示
    M5.Lcd.setCursor(50, 210);
    M5.Lcd.printf("CAL");
    M5.Lcd.setCursor(220, 210);
    M5.Lcd.printf("RESET");

    if (getIsButton()) {
      Serial.println(10);
    } else {
      Serial.println(-1);
    }

    M5.Lcd.fillCircle(160 + 80 * cos(degree), 120 + 80 * sin(degree), 10, BLACK);
    M5.Lcd.setCursor(160, 0);
    degree = (yaw - 90) / (180 / PI);
    GetGyro();
    M5.Lcd.drawCircle(160, 120, 80, WHITE);
    M5.Lcd.fillCircle(160 + 80 * cos(degree), 120 + 80 * sin(degree), 10, GREEN);
    M5.Lcd.printf("%4.0f", yaw);

    // 左の判定
    if (yaw < -60) {
      left_flag = 1;
      Serial.println(left_flag);
    }

    // 右の判定
    if (yaw > 60) {
      right_flag = 2;
      Serial.println(right_flag);
    }

    // 真ん中
    if ((yaw >= 0 && yaw < 20) || (yaw <= 0 && yaw > -20)) {
      center_flag = 0;
      Serial.println(center_flag);
    }

    if (M5.BtnA.wasPressed()) {
      calibration();
      Main();
    }

    if (M5.BtnC.wasPressed()) {
      ResetGyro();
      Main();
    }
  }
}

void setup() {
  M5.begin();
  M5.Power.begin();

  M5.IMU.Init();

  // ボタンのピン設定
  pinMode(26, INPUT);
  pinMode(36, INPUT);

  M5.Lcd.fillScreen(BLACK);
  M5.Lcd.setTextColor(WHITE, BLACK);
  M5.Lcd.setTextSize(2);
  delay(1);

  // 自動的にキャリブレーションとMainを実行
  calibration();
  Main();
}

void loop() {
  // ループなし
}
