using Godot;
using System;
using UIUtilities;
public partial class SaveDataPathBtn : Button
{
    [Signal]
    public delegate void CorrectDataPathEventHandler();

    [Export]
    public LineEdit DataFolderPathInput;
    [Export]
    public Label validatorLabel;
    [Export]
    public Control StartUpPanel;

    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton mb) {
            if (mb.ButtonIndex == MouseButton.Left && !mb.Pressed) {
                var response = Preferences.SetPath(DataFolderPathInput.Text);
                if (response.result) {
                    validatorLabel.Text = "";
                    EmitSignal(SignalName.CorrectDataPath);
                    StartUpPanel.Visible = false;
                } else {
                    validatorLabel.Text = response.validationText;
                }
            }
        }
    }

}
