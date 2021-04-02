using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Android.Util;

namespace RescueMaze_ScoreCalculator_Android
{
    [Activity(Label = "RB Score Calculator", MainLauncher = true, Icon = "@drawable/icon" ,ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        int Lags, Victims, SVictims, Barreirs, Result, ResPack, RampUp, RampDown, CheckPoints;
        Vibrator Vibe = (Vibrator)Application.Context.GetSystemService(VibratorService);
        System.Timers.Timer Timer;
        string TeamName, RoundNumber;
        int mins = 0, secs = 0, miliseconds = 0;
        bool Finished, SpecialScore, WasLongPress, doubleBackToExitPressedOnce;
        Button btnVicAdd;
        Button btnVicDel;
        Button btnCheckPointAdd;
        Button btnCheckPointDel;
        Button btnSVicAdd;
        Button btnSVicDel;
        Button btnLagAdd;
        Button btnLagDel;
        Button btnBariersAdd;
        Button btnBariersDel;
        Button btnPacksAdd;
        Button btntimeStartStop;
        Button btntimerReset;
        Button btnPacksDel;
        Button btnResult;
        CheckBox checkboxRampUp;
        CheckBox checkboxRampDown;
        CheckBox checkboxFinished;
        CheckBox checkboxSpecial;
        TextView lblVic;
        TextView lblSVic;
        TextView lblLag;
        TextView lblBarriers;
        TextView lblResPack;
        TextView lblCheckPoint;
        TextView lblTimer;

        protected override void OnCreate(Bundle bundle)
        {
            this.Title = "Rescue B Score Calculator";
            base.OnCreate(bundle);
            //View decorView = Window.DecorView;
            //var uiOptions = (int)decorView.SystemUiVisibility;
            //var newUiOptions = (int)uiOptions;
            //newUiOptions = (int)SystemUiFlags.HideNavigation;
            //newUiOptions = (int)SystemUiFlags.Immersive;
            //decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
            SetContentView(Resource.Layout.Main);
            //Initials
            btnVicAdd = FindViewById<Button>(Resource.Id.button1);
            btnVicDel = FindViewById<Button>(Resource.Id.button8);
            btnSVicAdd = FindViewById<Button>(Resource.Id.button2);
            btnSVicDel = FindViewById<Button>(Resource.Id.button9);
            btnLagAdd = FindViewById<Button>(Resource.Id.button3);
            btnLagDel = FindViewById<Button>(Resource.Id.button10);
            btnBariersAdd = FindViewById<Button>(Resource.Id.button4);
            btnBariersDel = FindViewById<Button>(Resource.Id.button11);
            btnPacksAdd = FindViewById<Button>(Resource.Id.button5);
            btnPacksDel = FindViewById<Button>(Resource.Id.button12);
            btnResult = FindViewById<Button>(Resource.Id.button6);
            btntimeStartStop = FindViewById<Button>(Resource.Id.button15);
            btntimerReset = FindViewById<Button>(Resource.Id.button14);
            btnCheckPointAdd = FindViewById<Button>(Resource.Id.button7);
            btnCheckPointDel = FindViewById<Button>(Resource.Id.button13);
            checkboxRampUp = FindViewById<CheckBox>(Resource.Id.checkBox4);
            checkboxRampDown = FindViewById<CheckBox>(Resource.Id.checkBox5);
            checkboxFinished = FindViewById<CheckBox>(Resource.Id.checkBox6);
            checkboxSpecial = FindViewById<CheckBox>(Resource.Id.checkBox7);
            lblVic = FindViewById<TextView>(Resource.Id.textView1);
            lblSVic = FindViewById<TextView>(Resource.Id.textView2);
            lblLag = FindViewById<TextView>(Resource.Id.textView3);
            lblCheckPoint = FindViewById<TextView>(Resource.Id.textView6);
            lblBarriers = FindViewById<TextView>(Resource.Id.textView4);
            lblResPack = FindViewById<TextView>(Resource.Id.textView5);
            lblTimer = FindViewById<TextView>(Resource.Id.textView8);
            btntimeStartStop.SetBackgroundColor(Android.Graphics.Color.LimeGreen);

            //Buttons
            
            btnVicAdd.Click += delegate { Vibe.Vibrate(20); if (Victims < 100) Victims++; lblVic.Text = string.Format("{0} Victims", Victims); };
            btnVicDel.Click += delegate { Vibe.Vibrate(20); if (Victims > 0) Victims--; lblVic.Text = string.Format("{0} Victims", Victims); };
            btnSVicAdd.Click += delegate { Vibe.Vibrate(20); if (SVictims < 100) SVictims++; lblSVic.Text = string.Format("{0} SVictims", SVictims); };
            btnSVicDel.Click += delegate { Vibe.Vibrate(20); if (SVictims > 0) SVictims--; lblSVic.Text = string.Format("{0} SVictims", SVictims); };
            btnLagAdd.Click += delegate { Vibe.Vibrate(20); if (Lags < 100) Lags++; lblLag.Text = string.Format("{0} Lack Of Progress", Lags); };
            btnLagDel.Click += delegate { Vibe.Vibrate(20); if (Lags > 0) Lags--; lblLag.Text = string.Format("{0} Lack Of Progress", Lags); };
            btnBariersAdd.Click += delegate { Vibe.Vibrate(20); if (Barreirs < 100) Barreirs++; lblBarriers.Text = string.Format("{0} Barriers", Barreirs); };
            btnBariersDel.Click += delegate { Vibe.Vibrate(20); if (Barreirs > 0) Barreirs--; lblBarriers.Text = string.Format("{0} Barriers", Barreirs); };
            btnCheckPointAdd.Click += delegate { Vibe.Vibrate(20); if (CheckPoints < 100) CheckPoints++; lblCheckPoint.Text = string.Format("{0} Check Points", CheckPoints); };
            btnCheckPointDel.Click += delegate { Vibe.Vibrate(20); if (CheckPoints > 0) CheckPoints--; lblCheckPoint.Text = string.Format("{0} Check Points", CheckPoints); };
            btnPacksAdd.Click += delegate { Vibe.Vibrate(20); if (ResPack < 100) ResPack++; lblResPack.Text = string.Format("{0} Rescue Kits", ResPack); };
            btnPacksDel.Click += delegate { Vibe.Vibrate(20); if (ResPack > 0) ResPack--; lblResPack.Text = string.Format("{0} Rescue Kits", ResPack); };

            btntimeStartStop.Click += delegate 
            {
                if (btntimeStartStop.Text == "Start") 
                {
                    Vibe.Vibrate(20);
                    CountDown();
                    btntimeStartStop.SetBackgroundColor(Android.Graphics.Color.OrangeRed);
                    btntimeStartStop.Text = "Stop";
                }
                else
                {
                    Vibe.Vibrate(20);
                    try
                    {
                        Timer.Stop();
                    }
                    catch { }
                    Timer = null;
                    lblTimer.Text = string.Format("{0}:{1:00}:{2:000}", mins, secs, miliseconds);
                    btntimeStartStop.SetBackgroundColor(Android.Graphics.Color.LimeGreen);
                    btntimeStartStop.Text = "Start";
                }
            };

            btntimerReset.Click += delegate
            {
                Vibe.Vibrate(20);
                try
                {
                    Timer.Stop();
                }
                catch { }
                Timer = null;
                btntimeStartStop.SetBackgroundColor(Android.Graphics.Color.LimeGreen);
                btntimeStartStop.Text = "Start";
                miliseconds = 0;
                secs = 0;
                mins = 0;
                lblTimer.Text = string.Format("{0}:{1:00}:{2:000}", mins, secs, miliseconds);
            };


            btnResult.Click += delegate 
            {
                Vibe.Vibrate(20);
                try
                {
                    Timer.Stop();
                }
                catch { }
                Timer = null;
                lblTimer.Text = string.Format("{0}:{1:00}:{2:000}", mins, secs, miliseconds);
                btntimeStartStop.SetBackgroundColor(Android.Graphics.Color.LimeGreen);
                btntimeStartStop.Text = "Start";
                CalculateResult();
                Toast.MakeText(this, "Your Score is: " + Result.ToString(), ToastLength.Short).Show();
            };

            //CheckBoxes
            checkboxRampUp.Click += (o, e) =>
            {
                Vibe.Vibrate(20);
                if (checkboxRampUp.Checked)
                    RampUp = 1;
                else
                    RampUp = 0;
            };

            checkboxRampDown.Click += (o, e) =>
            {
                Vibe.Vibrate(20);
                if (checkboxRampDown.Checked)
                    RampDown = 1;
                else
                    RampDown = 0;
            };

            checkboxFinished.Click += (o, e) =>
            {
                Vibe.Vibrate(20);
                if (checkboxFinished.Checked)
                    Finished = true;
                else
                    Finished = false;
            };

            checkboxSpecial.Click += (o, e) =>
            {
                Vibe.Vibrate(20);
                if (checkboxSpecial.Checked)
                    SpecialScore = true;
                else
                    SpecialScore = false; 
            };
            //decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
        }

        //public override void OnWindowFocusChanged(bool hasFocus)
        //{
        //    base.OnWindowFocusChanged(hasFocus);
        //    if (CurrentFocus != null)
        //    {
        //        View decorView = Window.DecorView;
        //        var uiOptions = (int)decorView.SystemUiVisibility;
        //        var newUiOptions = (int)uiOptions;
        //        newUiOptions = (int)SystemUiFlags.HideNavigation;
        //        newUiOptions = (int)SystemUiFlags.Immersive;
        //        decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
        //    }
        //}

        public override bool OnCreateOptionsMenu(IMenu Menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, Menu);
            return base.OnCreateOptionsMenu(Menu);
        }

        public void CalculateResult()
        {
            Result = (Victims * 10) + (SVictims * 25) + (ResPack * 10) + (Barreirs * 5) + (RampUp * 10) + (RampDown * 10) + (CheckPoints * 10);
            if (Finished) Result += ((Victims + SVictims) * 10);
            if (SpecialScore) Result += ((CheckPoints * 10) + ((Victims + SVictims) * 10) - (Lags * 10));
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.VolumeDown)
            {
                if (e.IsLongPress)
                {
                    Vibe.Vibrate(1000);
                    WasLongPress = true;
                    ResetData();
                }

                return true;
            }

            if (keyCode == Keycode.VolumeUp)
                return true;
            return base.OnKeyDown(keyCode, e);
        }

        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.VolumeUp)
            {
                if (btntimeStartStop.Text == "Start")
                {
                    CountDown();
                    btntimeStartStop.SetBackgroundColor(Android.Graphics.Color.OrangeRed);
                    btntimeStartStop.Text = "Stop";
                    Vibe.Vibrate(85);
                }
                else
                {
                    try
                    {
                        Timer.Stop();
                    }
                    catch { }
                    Timer = null;
                    lblTimer.Text = string.Format("{0}:{1:00}:{2:000}", mins, secs, miliseconds);
                    btntimeStartStop.SetBackgroundColor(Android.Graphics.Color.LimeGreen);
                    btntimeStartStop.Text = "Start";
                    Vibe.Vibrate(85);
                }
                return true;
            }

             if (keyCode == Keycode.VolumeDown && !WasLongPress)
            {
                try
                {
                    Timer.Stop();
                }
                catch { }
                Timer = null;
                btntimeStartStop.SetBackgroundColor(Android.Graphics.Color.LimeGreen);
                btntimeStartStop.Text = "Start";
                miliseconds = 0;
                secs = 0;
                mins = 0;
                lblTimer.Text = string.Format("{0}:{1:00}:{2:000}", mins, secs, miliseconds);
                Vibe.Vibrate(300);
                return true;
            }

            WasLongPress = false;
            return base.OnKeyUp(keyCode, e);
        }

        public override void OnBackPressed()
        {
            if (doubleBackToExitPressedOnce)
            {
                base.OnBackPressed();
                Java.Lang.JavaSystem.Exit(0);
                return;
            }


            this.doubleBackToExitPressedOnce = true;
            Toast.MakeText(this, "Please Press BACK Again To Exit.", ToastLength.Short).Show();

            new Handler().PostDelayed(() =>
            {
                doubleBackToExitPressedOnce = false;
            }, 1500);
        }

        private void CountDown()
        {
            Timer = new System.Timers.Timer();
            Timer.Interval = 1;
            Timer.Elapsed += OnTimedEvent;
            Timer.Start();
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            miliseconds++;
            if (miliseconds >= 1000) 
            {
                secs++;
                miliseconds = 0;
            }

            if (secs == 59) 
            {
                mins++;
                secs = 0;
            }
            RunOnUiThread(() => { lblTimer.Text = string.Format("{0}:{1:00}:{2:000}", mins, secs, miliseconds); });
        }

        public interface ISaveAndLoad
        {
            void SaveText(string filename, string text);
            string LoadText(string filename);
        }

        public void SaveScore()
        {
            var path = Android.OS.Environment.ExternalStorageDirectory + @"/RB Score Calculator";
            var filename = Path.Combine(path.ToString(), "Scores.txt");

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    using (var streamWriter = new StreamWriter(filename, true))
                    {
                        streamWriter.WriteLine("Team    " + "Round  " + "Score  " + "Duration");
                        streamWriter.WriteLine("  ");
                    }
                }

                    CalculateResult();

                    LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
                    View mView = layoutInflaterAndroid.Inflate(Resource.Layout.User_Input_Dialog_Box_TeamName, null);
                    AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
                    alertDialogBuilder.SetView(mView);

                    var userContent = mView.FindViewById<EditText>(Resource.Id.editText1);
                    alertDialogBuilder.SetCancelable(false)
                    .SetPositiveButton("Next", delegate
                     {
                         TeamName = userContent.Text;
                         LayoutInflater layoutInflaterAndroid_ = LayoutInflater.From(this);
                         View mView_ = layoutInflaterAndroid.Inflate(Resource.Layout.User_Input_Dialog_Box_RoundNumber, null);
                         AlertDialog.Builder alertDialogBuilder_ = new AlertDialog.Builder(this);
                         alertDialogBuilder_.SetView(mView_);

                         var userContent_ = mView_.FindViewById<EditText>(Resource.Id.editNumber1);
                         alertDialogBuilder_.SetCancelable(false)
                         .SetPositiveButton("Save", delegate
                         {
                             RoundNumber = userContent_.Text;
                             using (var streamWriter = new StreamWriter(filename, true))
                             {
                                 streamWriter.WriteLine(TeamName + "    " + RoundNumber + "    " + Result.ToString() + "    " + lblTimer.Text);
         
                                 Toast.MakeText(this, "Saved To: "+ filename.ToString(), ToastLength.Long).Show();
                             }
                         })
                         .SetNegativeButton("Cancel", delegate
                         {
                             alertDialogBuilder_.Dispose();
                         });

                         AlertDialog alertDialog_ = alertDialogBuilder_.Create();
                         alertDialog_.Show();
                     })
                    .SetNegativeButton("Cancel", delegate
                     {
                         alertDialogBuilder.Dispose();
                     });

                    AlertDialog alertDialog = alertDialogBuilder.Create();
                    alertDialog.Show();

                
            }
            catch (Exception e)
            {
                Toast.MakeText(this, e.Message.ToString(), ToastLength.Long).Show();
            }
        }
        

        public void ResetData()
        {
            Lags = 0;
            Victims = 0;
            SVictims = 0;
            Barreirs = 0;
            Result = 0;
            ResPack = 0;
            CheckPoints = 0;
            RampUp = 0;
            RampDown = 0;
            Finished = false;
            SpecialScore = false;
            checkboxRampUp.Checked = false;
            checkboxRampDown.Checked = false;
            checkboxFinished.Checked = false;
            checkboxSpecial.Checked = false;
            lblVic.Text = "0 Victims";
            lblSVic.Text = "0 SVictims";
            lblLag.Text = "0 Lack Of Progress";
            lblBarriers.Text = "0 Barriers";
            lblResPack.Text = "0 Rescue Kits";
            lblCheckPoint.Text = "0 Check Points";
            try
            {
                Timer.Stop();
            }
            catch { }
            Timer = null;
            btntimeStartStop.SetBackgroundColor(Android.Graphics.Color.LimeGreen);
            btntimeStartStop.Text = "Start";
            miliseconds = 0;
            secs = 0;
            mins = 0;
            lblTimer.Text = string.Format("{0}:{1:00}:{2:000}", mins, secs, miliseconds);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Resource.Id.itemReset:
                    Vibe.Vibrate(20);
                    ResetData();
                    return true;

                case Resource.Id.itemSave:
                    Vibe.Vibrate(20);
                    try
                    {
                        Timer.Stop();
                    }
                    catch { }
                    Timer = null;
                    lblTimer.Text = string.Format("{0}:{1:00}:{2:000}", mins, secs, miliseconds);
                    btntimeStartStop.SetBackgroundColor(Android.Graphics.Color.LimeGreen);
                    btntimeStartStop.Text = "Start";
                    SaveScore();
                    return true;

                case Resource.Id.itemAbout:
                    Vibe.Vibrate(20);
                    ShowAlert("RB Score Calculator V4.0","Developed By :"+"\n"+"       Mohammad Reza Anvari"+"\n\n"+"@Mrezanvari"+"\n"+"Mrezanvari@gmail.com"+"\n\n\n"+ "© 2017-2020 Mrezanvari All Rights Reserved" + "\n\n");
                    return true;

                case Resource.Id.itemExit:
                    Vibe.Vibrate(50);
                    Java.Lang.JavaSystem.Exit(0);
                    return true;

                default:

                    return base.OnOptionsItemSelected(item);
            }
        }

        
        public void ShowAlert(string str,string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(str);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) => {});
            RunOnUiThread(() => { alert.Show(); });
        }
    }
}