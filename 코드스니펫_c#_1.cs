        // Ctrl+C(룩업 항목 복사) : 마우스 드래그드랍의 로직을 같이 사용함.
        private void gridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 문자입력에 대한 체크
            try
            {
                // 한글,영문,숫자 입력 감지 ( 제어문자 제외 )
                if (IsTextInputCharacter(e.KeyChar))
                {
                    MoveToTextBox(e.KeyChar);
                    //e.Handled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                LoggerUtil.Error("Fnguide.DataGuide.Controls.DgDevControls.DgDevCodeLookup", ex);
                return;
            }

            // 복사/붙여넣기에 대한 처리
            try
            {
                if (gridCode.DragMode >= DragModeEnum.FnAutomatic)
                {
                    if (e.KeyChar == (char)3) // Ctrl+C
                    {
                        DataObject oDataObjectCopy;
                        if (this.EventCollectionName == "CODE_LOOKUP")
                            oDataObjectCopy = SetSelectedDataCodeLookup(true);
                        else
                            oDataObjectCopy = SetSelectedDataItemLookup(true);

                        if (oDataObjectCopy != null)
                        {
                            // DataObject에서 Dictionary<string, string> 데이터 추출
                            var dicToGrid = (Dictionary<string, string>)oDataObjectCopy.GetData(typeof(Dictionary<string, string>));

                            if (dicToGrid != null && dicToGrid.ContainsKey("DRAG"))
                            {
                                //string gridData = "*" + dicToGrid["RCSET"] + "*" + dicToGrid["DRAG"];
                                string gridData = dicToGrid["DRAG"];
                                //string gridData = dicToGrid["DRAG"];

                                if (this.InvokeRequired)
                                {
                                    this.Invoke(new Action(() => Clipboard.SetText(gridData)));
                                }
                                else
                                {
                                    Clipboard.SetText(gridData);
                                }
                            }
                        }
                    }
                    // 붙여넣기는 그리드컨트롤 객체에서 처리함. > 이곳은 룩업용 그리드입니다. 붙여넣기는 대상 그리드가 틀림.

                }

            }
            catch (Exception ex)
            {
                LoggerUtil.Error("Fnguide.DataGuide.Controls.DgDevControls.DgDevCodeLookup", ex);
            }

        }
