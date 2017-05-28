using UnityEngine;
using System.Collections;
using System;

public class MenuManager : MonoBehaviour {

    public static MenuManager m;

    public Menu[] menus;

    public string defaultMenu = "gameplay";

    [Serializable]
    // note: I could use hashmap, but i want to edit keys in inspector, and there won't be many menus anyway.
    public class Menu {
        public string key;
        public Transform menuRef;

        public void Show(bool visible) {
            menuRef.gameObject.SetActive(visible);
        }
    }

    void Awake() {
        m = this;
        // default menu
        ShowMenu(defaultMenu);
    }

    /// <summary>
    /// Show target menu and hide the rest
    /// </summary>
    /// <param name="v"></param>
    public void ShowMenu(string target) {
        for (int i = 0; i < menus.Length; i++) {
            if (menus[i].key == target) {
                menus[i].Show(true);
            }
            else menus[i].Show(false);
        }
    }
}
