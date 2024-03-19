using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IArrowHittable {
    private float internalCircleRadius = 0.037f;
    private float externalRadius = 0.49f;
    private float stripeWidth;
    private int numberOfStripes = 9;

    void Start() {
        stripeWidth = (externalRadius - internalCircleRadius) / numberOfStripes;
    }

    public void Hit(Arrow arrow, RaycastHit hit) {
        //Debug.Log("[Target] " + hit.point);

        Vector3 impactPoint = transform.InverseTransformPoint(hit.point);
        //Poñemos a coordenada x a 0 para que o punto de impacto quede no plano central da diana
        impactPoint.x = 0;
        float impactDistance = impactPoint.magnitude;
        int points = 0;
        if(impactDistance <= internalCircleRadius){
            points = 10;
        } else if(impactDistance <= externalRadius){
            //calculamos a distancia dende o borde do circulo interior
            impactDistance -= internalCircleRadius;
            //calculamos o índice da franxa na que cae a impactDistance, contando dende 0
            //e empezando pola franxa máis achegada ó círculo central
            int stripeIndex = (int)Mathf.Floor(impactDistance/stripeWidth);
            //Os puntos son o número de franxas menos o índice
            points = numberOfStripes - stripeIndex;
            //Debug.Log("[Target] Hit puntos acadados  " + points);

            GameManager.instance.Score(points);
        }
    }
}
