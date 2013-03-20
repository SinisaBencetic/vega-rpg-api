using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaRpgAi
{
    abstract class Goal
    {
        public virtual bool IsEqualTo(Goal target);
        public Emotion RullingEmotion { get; set; }
        private bool wasBeneficient;
        /// <summary>
        /// Only for archived goals! Gets or sets a value indicating whether [was beneficent].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [was beneficent]; otherwise, <c>false</c>.
        /// </value>
        public bool WasBeneficent { 
            get{
                return wasBeneficient;
            } 
            set {
                if (!this.IsArchived) throw new ApplicationException("Can't set beneficiancy on goal that is not archived");
                wasBeneficient = value;        
            } 
        }
        /// <summary>
        /// Gets or sets a value indicating whether this Goal instance is archived.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is archived; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchived { get; set; }
    }
}
