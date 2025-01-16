
using System;
using UnityEngine;
#if PLATFORM_IOS
using Unity.Notifications.iOS;
#else
//using Unity.Notifications.Android;
#endif

namespace TinyStudio
{
    public class NotificationController : MonoBehaviour
    {
        //Values
        public bool isActiveHere = true; //Make active only at launch scene

        [Header("Notification values")]
        public string notification_1_title = "Hey!";
        public string notification_2_title = "Hey!";
        public string notification_3_title = "Hey!";
        public string[] notification_1_text;
        public string[] notification_2_text;
        public string[] notification_3_text;
        public int notification_1_delayHR = 2;
        public int notification_2_delayHR = 6;
        public int notification_3_delayHR = 22;

        #region Standart system methods

        private void Start()
        {
            if (isActiveHere)
            {
                GenerateNotifications();
            }
        }

        #endregion

        /// <summary>
        /// Generate notification if it's possible by settings paramether.
        /// </summary>
        public void GenerateNotifications()
        {
            if (CustomPlayerPrefs.GetBool("notification"))
            {
                ScheduleNotiification("notification_1",
                    notification_1_title,
                    notification_1_text[UnityEngine.Random.Range(0, notification_1_text.Length)],
                    notification_1_delayHR);
                ScheduleNotiification("notification_2",
                    notification_2_title,
                    notification_2_text[UnityEngine.Random.Range(0, notification_2_text.Length)],
                    notification_2_delayHR);
                ScheduleNotiification("notification_3",
                    notification_3_title,
                    notification_3_text[UnityEngine.Random.Range(0, notification_3_text.Length)],
                    notification_3_delayHR);
            }
            else
            {
                CancelAllNotifications();
            }
        }

        /// <summary>
        /// Create notification. Not depent by platform version
        /// </summary>
        /// <param name="id">Notofication id</param>
        /// <param name="title">Title of notification</param>
        /// <param name="text">Text of notification</param>
        /// <param name="afterHours">Time when show notification</param>
        private void ScheduleNotiification(string id, string title, string text, int afterHours)
        {
#if PLATFORM_IOS
            ScheduleIOSNotification(id, title, text, afterHours);
#else
        //AndroidNotificationCenter.CancelAllNotifications();
        ScheduleAndroidNotification(id, title, text, afterHours);
#endif
        }

        /// <summary>
        /// Process notification creation in iOS system.
        /// </summary>
        private void ScheduleIOSNotification(string id, string title, string text, int afterHours)
        {
#if PLATFORM_IOS
            iOSNotificationCenter.RemoveScheduledNotification(id);

            var timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(afterHours, 0, 0),
                Repeats = false
            };

            var notification = new iOSNotification()
            {
                Identifier = id,
                Title = title,
                Body = text,
                Subtitle = "",
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                CategoryIdentifier = "category_a",
                ThreadIdentifier = "thread1",
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
#endif
        }

        /// <summary>
        /// Process notification creation in Androind system.
        /// </summary>
        private void ScheduleAndroidNotification(string id, string title, string text, int afterHours)
        {
#if PLATFORM_IOS
#else
        //var c = new AndroidNotificationChannel()
        //{
        //    Id = id,
        //    Name = "Default Channel",
        //    //Importance = Importance.High,
        //    Description = "Generic notifications",
        //};
        //AndroidNotificationCenter.RegisterNotificationChannel(c);

        //var notification = new AndroidNotification();
        //notification.Title = title;
        //notification.Text = text;
        //notification.FireTime = System.DateTime.Now.AddHours(afterHours);

        //AndroidNotificationCenter.SendNotification(notification, id);
#endif
        }

        /// <summary>
        /// Cancel all notifications. Not depent by platform version
        /// </summary>
        public void CancelAllNotifications()
        {
#if PLATFORM_IOS
            iOSNotificationCenter.RemoveAllScheduledNotifications();
#else
        //AndroidNotificationCenter.CancelAllNotifications();
#endif
        }
    }
}