﻿//======================================================================
//        Copyright (C) 2015-2020 Winddy He. All rights reserved
//        Email: hgplan@126.com
//======================================================================
using Framework.WindUI;
using UnityEngine.UI;
using Framework.Hotfix;
using WindHotfix.GUI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Game.Knight
{
    public class CreatePlayerView : TUIViewController<CreatePlayerView>
    {
        public ToggleGroup              ProfessionSelected;
        public InputField               PlayerName;
        public Text                     ProfessionalDesc;

        public CreatePlayerItem         CurrentSelectedItem;
        public List<CreatePlayerItem>   CreatePlayerItems;
        
        public override void OnInitialize()
        {
            // 转换变量
            this.ProfessionSelected  = this.Objects[0].Object as ToggleGroup;
            this.PlayerName          = this.Objects[1].Object as InputField;
            this.ProfessionalDesc    = this.Objects[2].Object as Text;

            // 初始化Items
            this.InitItems();

            // 注册事件
            this.EventBinding(this.Objects[4].Object, EventTriggerType.PointerClick, OnPlayerCreateBtn_Clicked);
            this.EventBinding(this.Objects[5].Object, EventTriggerType.PointerClick, OnBackBtn_Clicked);
        }
        
        private void InitItems()
        {
            this.CreatePlayerItems = new List<CreatePlayerItem>();

            CreatePlayerItem rItem = new CreatePlayerItem();
            rItem.Initialize(this, this.Objects[6].Object as HotfixMBContainer);
            this.CreatePlayerItems.Add(rItem);

            rItem = new CreatePlayerItem();
            rItem.Initialize(this, this.Objects[7].Object as HotfixMBContainer);
            this.CreatePlayerItems.Add(rItem);

            rItem = new CreatePlayerItem();
            rItem.Initialize(this, this.Objects[8].Object as HotfixMBContainer);
            this.CreatePlayerItems.Add(rItem);
        }

        public override void OnOpening()
        {
            this.CurrentSelectedItem = this.CreatePlayerItems[0];
            this.CurrentSelectedItem.StartLoad();
            this.mIsOpened = true;
        }

        public override void OnClosing()
        {
            this.CurrentSelectedItem.StopLoad();
            this.mIsClosed = true;

            this.EventUnBinding(this.Objects[4].Object, EventTriggerType.PointerClick, OnPlayerCreateBtn_Clicked);
            this.EventUnBinding(this.Objects[5].Object, EventTriggerType.PointerClick, OnBackBtn_Clicked);

            for (int i = 0; i < this.CreatePlayerItems.Count; i++)
            {
                this.CreatePlayerItems[i].Destroy();
            }
        }

        private void OnPlayerCreateBtn_Clicked(UnityEngine.Object rTarget)
        {
            if (string.IsNullOrEmpty(this.PlayerName.text))
            {
                Toast.Instance.Show("角色名不能为空！");
                return;
            }
            Game.Knight.CreatePlayer.Instance.Create(this.PlayerName.text, this.CurrentSelectedItem.ProfessionalID);
        }

        private void OnBackBtn_Clicked(UnityEngine.Object rTarget)
        {
            UIViewManager.Instance.Open("KNPlayerList", UIView.State.dispatch);
        }
    }
}
