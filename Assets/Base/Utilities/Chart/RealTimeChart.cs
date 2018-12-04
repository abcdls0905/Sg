using System;
using System.Collections.Generic;
using UnityEngine;
namespace GameUtil
{
	public class RealTimeChart : MonoSingleton<RealTimeChart>
	{
        private List<Track> Tracks = new List<Track>();
		private bool bVisible;
		//private System.Random RandomSupport = new System.Random();
		public bool isVisible
		{
			get
			{
				return this.bVisible;
			}
			set
			{
				this.bVisible = value;
			}
		}
		public Track FindTrack(string InTag)
		{
			for (int i = 0; i < this.Tracks.Count; i++)
			{
				if (this.Tracks[i].tag == InTag)
				{
					return this.Tracks[i];
				}
			}
			return null;
		}
		public void RemoveTrack(string InTag)
		{
			for (int i = 0; i < this.Tracks.Count; i++)
			{
				if (this.Tracks[i].tag == InTag)
				{
					this.Tracks.RemoveAt(i);
					break;
				}
			}
		}

        public void RemoveAllTrack()
        {
            Tracks.Clear();
        }
		public void RemoveTrack(Track InTrack)
		{
			if (InTrack != null)
			{
				this.RemoveTrack(InTrack.tag);
			}
		}
		public Track AddTrack(string InTag, Color InColor)
		{
			Track track = this.FindTrack(InTag);
			if (track == null)
			{
				track = new Track(InTag, InColor);
                this.Tracks.Add(track);
			}
			else
			{
				track.drawColor = InColor;
			}
			return track;
		}

        public Track AddTrack(string InTag, Color InColor, bool bFixedRange, float InMin, float InMax, int cellnum)
		{
			Track track = this.FindTrack(InTag);
			if (track == null)
			{
				track = new Track(InTag, InColor, InMin, InMax);
				this.Tracks.Add(track);
			}
			else
			{
				track.drawColor = InColor;
			}
			track.SetFixedRange(bFixedRange, InMin, InMax);
            track.cellnum = cellnum;
            return track;
		}
		public void AddSample(string InTag, float InValue)
		{
			Track track = this.FindTrack(InTag);
			if (track != null)
			{
				track.AddSample(InValue);
			}
			else
			{
				//Debug.Assert(false, "no valid track with tag");
			}
		}
		public void SetSample(string InTag, float InValue, int reverseIndex)
		{
			Track track = this.FindTrack(InTag);
			if (track != null)
			{
				track.SetSample(InValue, reverseIndex);
			}
			else
			{
				//Debug.Assert(false, "no valid track with tag");
			}
		}
		private void OnGUI()
		{
			if (!this.bVisible)
			{
				return;
			}
			this.DrawBase();
			this.DrawTracks();
            Drawing.RenderLines();
		}
		protected void DrawBase()
		{
			Track.DrawLine(new Vector2(0f, 0f), new Vector2(0f, (float)Screen.height * (1f - Track.Clip * 2f)), Color.white, 2f);
			Track.DrawLine(new Vector2(0f, 0f), new Vector2((float)Screen.width * (1f - Track.Clip * 2f), 0f), Color.white, 2f);
            Track.DrawLine(new Vector2((float)Screen.width * (1f - Track.Clip * 2f), 0f), new Vector2((float)Screen.width * (1f - Track.Clip * 2f), (float)Screen.height * (1f - Track.Clip * 2f)), Color.white, 2f);
            Track.DrawLine(new Vector2(0f, (float)Screen.height * (1f - Track.Clip * 2f)), new Vector2((float)Screen.width * (1f - Track.Clip * 2f), (float)Screen.height * (1f - Track.Clip * 2f)), Color.white, 2f);
        }
		protected void DrawTracks()
		{
            for (int j = 0; j < this.Tracks.Count; j++)
            {
                if (this.Tracks[j].hasSamples && this.Tracks[j].isVisiable)
                {
                    this.Tracks[j].OnRender(j, Tracks.Count);
                }
            }
        }
	}
}
