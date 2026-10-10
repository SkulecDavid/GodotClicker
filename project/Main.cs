using Godot;
using System;

public partial class Main : Node2D
{
	int score;
	double percent;
	bool isCooked;
	int alarmCount;

    TextureButton btnMain;
	RichTextLabel lblScore;
	RichTextLabel lblPercent;
	ColorRect alarmColor;
	Timer alarmTimer;

	public override void _Ready()
	{
		score = 0;
		percent = 0.0;
		isCooked = false;
		alarmCount = 0;

        btnMain = GetNode<TextureButton>("BtnMain");
		lblScore = GetNode<RichTextLabel>("LblScore");
		lblPercent = GetNode<RichTextLabel>("LblPercent");
		alarmColor = GetNode<ColorRect>("AlarmColor");
		alarmTimer = GetNode<Timer>("AlarmTimer");

		alarmTimer.WaitTime = 1.0;
		alarmTimer.OneShot = false;
		alarmTimer.Timeout += OnAlarmTimeout;

        btnMain.Pressed += OnButtonPressed;
	}

	void OnButtonPressed()
	{
		//GD.Print("OK");
		double rnd = new Random().NextDouble() * 100;
		//GD.Print(rnd);
        if (percent >= rnd)
		{
            GD.Print("Boom!");
			btnMain.Disabled = true;
			isCooked = true;
        }

		score++;
		lblScore.Text = score.ToString();
		percent += 0.001;
		lblPercent.Text = percent.ToString("F3") + "%";
	}

	void StartAlarm()
	{
		alarmColor.Visible = true;
		alarmTimer.Start();
	}

	private void OnAlarmTimeout()
	{
		if (alarmCount < 20)
		{
            alarmColor.Visible = !alarmColor.Visible;
            alarmCount++;
        }
	}

    public override void _Process(double delta)
	{
		if (isCooked)
		{
			StartAlarm();
			isCooked = false;
        }
    }
}
