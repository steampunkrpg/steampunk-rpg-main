#pragma strict

private static var instance;

 public static function GetInstance() {
     return instance;
 }
  
 function Awake() {
     if (instance != null && instance != this) {
         Destroy(this.gameObject);
         return;
     } else {
         instance = this;
     }
     DontDestroyOnLoad(this.gameObject);
 }
