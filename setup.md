# Android Setup Guide for MiniDoctor

This is the exact setup used to make **MiniDoctor** export to Android successfully.  
Follow these steps to replicate the working environment.

---

## 1. Install Android Studio
- Installed **Android Studio Ladybug | 2024.2.1** (latest stable at the time).
- Selected components:
  - **Android SDK**
  - **Android SDK Platform-Tools**
  - **Android SDK Build-Tools**
  - **NDK (Side by side)**
  - **OpenJDK (bundled)**

---

## 2. SDK/NDK/JDK Paths
On Windows, Android Studio placed the SDK at:

```
C:\Users\<YourName>\AppData\Local\Android\Sdk
```

Confirmed installed components:
- **Build Tools:** `34.0.0`  
- **NDK:** `25.2.9519653`  
- **Platform:** `android-34`  
- **JDK:** auto-detected from Android Studio’s bundled JDK.

In Godot (`Editor → Editor Settings → Export → Android`):
- **Android SDK Path:**  
  `C:/Users/<YourName>/AppData/Local/Android/Sdk`
- **JDK Path:** auto-detected (left default).
- **Debug Keystore:** (see next step).

---

## 3. Debug Keystore
Generated a debug keystore with:

```bash
keytool -genkeypair -v -keystore debug.keystore -storepass android -keypass android -alias androiddebugkey -keyalg RSA -keysize 2048 -validity 10000
```

- File stored at:  
  `C:\workspace\Android\keystores\debug.keystore`

- Godot `Editor Settings → Export → Android`:
  - **Debug Keystore:** `C:/workspace/Android/keystores/debug.keystore`
  - **Debug Keystore User:** `androiddebugkey`
  - **Debug Keystore Password:** `android`

---

## 4. Godot Export Preset
In `Project → Export`:
- Added **Android preset**.
- Package name:  
  `com.minidoctor.game`
- Version name: `1.0`  
- Version code: `1`
- Minimum SDK: `21`
- Target SDK: `34`
- Keystore section auto-filled from above.

---

## 5. Test Build
- First export produced a working `.apk` file.
- Installed and ran successfully on test Android device.

---

## 6. Release Keystore (later)
When ready to publish, generate a separate release keystore (with different password + alias).  
For now, the debug keystore is enough for development and testing.

---

✅ With this setup, MiniDoctor builds and runs on Android with no additional steps.
