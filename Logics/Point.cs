namespace Ex02_Othello.Logics
{
    public class Point
    {
        // $G$ CSS-999 (-5) local members should start with m_PascaleCased
        private int m_x;
        private int m_y;

        public Point(int x, int y)
        {
            this.m_x = x;
            this.m_y = y;
        }

        public int XValue
        {
            get
            {
                return m_x;
            }

            set
            {
                m_x = value;
            }
        }

        public int YValue
        {
            get
            {
                return m_y;
            }

            set
            {
                m_y = value;
            }
        }
    }
}
