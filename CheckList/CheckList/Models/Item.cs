using System;
using com.kmd.Helpers;

namespace com.kmd.Models {
    public class Item : BaseDataObject {
        string text = string.Empty;
        public string Text {
            get { return text; }
            set { SetProperty (ref text, value); }
        }

        string description = string.Empty;
        public string Description {
            get { return description; }
            set { SetProperty (ref description, value); }
        }

        public Item () {
            this.Created = DateTime.UtcNow;
        }

        DateTime created;
        public DateTime Created {
            get {
                return created;
            }
            set {
                created = value;
            }
        }

        DateTime completed;
        public DateTime Completed {
            get {
                return completed;
            }
            set {
                completed = value;
            }
        }

        int status;
        public ItemStatus Status {
            get {
                return (ItemStatus) status;
            }
            set {

                switch (value) {

                    case ItemStatus.Pending:
                        //switching from In-Progress to Pending             | Halting the task
                        if (status == (int) ItemStatus.Processing)
                            timeInProgress += DateTime.UtcNow.Subtract (LastModifiedOn);
                        break;

                    case ItemStatus.Processing:
                        //Switching from Pending to In-Progress             | Start progress
                        if (status == (int) ItemStatus.Pending) {
                            timePending += DateTime.UtcNow.Subtract (LastModifiedOn);
                            timeStarted = DateTime.UtcNow;
                        }
                        break;

                    case ItemStatus.Completed:
                        timeFinished = DateTime.UtcNow;

                        //Switching from Pending to Completed               | Instant finish
                        if (status == (int) ItemStatus.Pending) {
                            timePending += DateTime.UtcNow.Subtract (LastModifiedOn);
                            timeInProgress = TimeSpan.MinValue;
                        }

                        //Switching from In-Progress to Completed           | Standard finish
                        if (status == (int) ItemStatus.Processing)
                            timeInProgress += DateTime.UtcNow.Subtract (LastModifiedOn);

                        TotalTime = timePending + timeInProgress;
                        break;
                }

                status = (int) value;
                LastModifiedOn = DateTime.UtcNow;
            }
        }

        TimeSpan timePending;
        TimeSpan timeInProgress;
        DateTime timeStarted;
        DateTime timeFinished;

        DateTime lastModifiedOn;
        public DateTime LastModifiedOn {
            get {
                if (lastModifiedOn == default (DateTime))
                    lastModifiedOn = Created;
                return lastModifiedOn;
            }
            set {
                lastModifiedOn = value;
            }
        }

        TimeSpan time;
        public TimeSpan TotalTime {
            get {
                //if(Status == ItemStatus.Completed)
                //    return time;
                //return default (TimeSpan);
                return timePending + timeInProgress;
            }
            set {
                time = value;
            }
        }

        public TimeSpan TimeInCurrentStatus {
            get {
                return DateTime.UtcNow.Subtract (LastModifiedOn);
            }
        }

        public bool IsFinished => this.Status == ItemStatus.Completed;
        public bool IsInProgress => this.Status == ItemStatus.Processing;
        public bool IsPending => this.Status == ItemStatus.Pending;

        public bool CanShowStatus => this.Status != ItemStatus.Completed;
        public bool CanShowStop => !this.IsFinished;

        public string StatusIcon {
            get {
                switch (this.Status) {
                    case Item.ItemStatus.Pending:
                        return "Resume.png";
                    case Item.ItemStatus.Processing:
                        return "Pause.png";
                    case Item.ItemStatus.Completed:
                        return "Success.png";
                }
                return "Success.png";
            }
        }

        public enum ItemStatus {
            Pending,
            Processing,
            Completed
        }

        #region Verbose

        public string VCreated {
            get {
                return DateTime.UtcNow.Subtract (Created).ToPrettyString () + " ago.";
            }
        }

        public string VLastModifiedOn {
            get {
                return DateTime.UtcNow.Subtract (LastModifiedOn).ToPrettyString () + " ago.";
            }
        }

        public string VTotalTime {
            get {
                if (Status == ItemStatus.Completed)
                    return this.time.ToPrettyString ();
                return "-";
            }
        }
        public string VTimeInCurrentStatus {
            get {
                return this.TimeInCurrentStatus.ToPrettyString ();
            }
        }
        #endregion
    }
}
