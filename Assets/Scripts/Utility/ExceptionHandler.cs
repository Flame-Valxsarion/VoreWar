using System.IO;
using UnityEngine;

public class ExceptionHandler : MonoBehaviour
{

    int exceptionCount;
    string path;

    void Awake()
    {
        if (Application.isEditor)
            return;

        Application.logMessageReceived += HandleException;
        if (Application.platform == RuntimePlatform.OSXPlayer)
            path = Path.Combine(Application.persistentDataPath, "recentexceptions.txt");
        else
            path = Path.Combine(Application.dataPath, "recentexceptions.txt");
    }

    private void HandleException(string condition, string stackTrace, LogType type)
    {
        try
        {
            if (type == LogType.Exception)
            {
                exceptionCount++;
				if (exceptionCount == 1)
				{
					if (Application.platform == RuntimePlatform.OSXPlayer)
						State.GameManager.CreateFullScreenMessageBox($"The first Exception of this session was just logged to recentexceptions.txt, you'll probably want to notify a dev on the VoreWar Discord or GitHub with the contents of that file so it can be fixed.  As this is a mac and mac exception logs don't seem to be writing correctly at the moment, you can take a screenshot of this screen and send it instead.  \nFull Details: {type}: {condition}\nVersion :{State.Version}\n{stackTrace}");
					else
						State.GameManager.CreateMessageBox("The first Exception of this session was just logged to VoreWar_Data/recentexceptions.txt, you'll probably want to notify a dev on the VoreWar Discord or GitHub with the contents of that file so it can be fixed.\nPlease note that the game is now in an unstable state due to this error.  Any other errors following this message may not be reliable as they could be symptoms of the original error.");
					
					using (StreamWriter writer = new StreamWriter(path))
					{
						writer.WriteLine($"{type}: {condition}\nVersion :{State.Version}\n{stackTrace}\n\n");
						writer.Flush();
					}
					return;
				}
				else if (exceptionCount == 50) //To avoid too much clutter
				{
					State.GameManager.CreateMessageBox("The fiftieth Exception of this session was just logged to recentexceptions.txt, no further exceptions will be logged this session.");
				}
				else if (exceptionCount > 50)
				{
					return;
				}
				
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine($"{type}: {condition}\nVersion :{State.Version}\n{stackTrace}\n\n");
                    writer.Flush();
                }
            }
        }
        catch
        {
            Debug.Log("Failed to write exception... just eating it rather than throw another exception");
        }
    }
}
